#!/bin/sh
set -e

echo "Applying Billing migrations..."
./migrate-billing --connection "$ConnectionStrings__DefaultConnection"

echo "Applying Identity migrations..."
./migrate-identity --connection "$ConnectionStrings__DefaultConnection"

echo "Applying Features migrations..."
./migrate-features --connection "$ConnectionStrings__DefaultConnection"

echo "Migrations up to date. Starting API..."
exec dotnet MySaaS.Api.dll