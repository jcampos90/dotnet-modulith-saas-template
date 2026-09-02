FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish src/Identity/Identity.PublicApi -c Release -o /app/publish

# --- Migration bundles: one per module context ---
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT=Production
RUN dotnet ef migrations bundle \
      --context BillingDbContext \
      --project src/Billing/Billing.Infrastructure \
      --startup-project src/Api/MySaaS.Api \
      --self-contained -r linux-x64 -o /app/publish/migrate-billing
RUN dotnet ef migrations bundle \
      --context IdentityDbContext \
      --project src/Identity/Identity.Infrastructure \
      --startup-project src/Api/MySaaS.Api \
      --self-contained -r linux-x64 -o /app/publish/migrate-identity
RUN dotnet ef migrations bundle \
      --context FeaturesDbContext \
      --project src/Features/Features.Infrastructure \
      --startup-project src/Api/MySaaS.Api \
      --self-contained -r linux-x64 -o /app/publish/migrate-features


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh migrate-billing migrate-identity migrate-features
EXPOSE 8080
ENTRYPOINT ["./entrypoint.sh"]