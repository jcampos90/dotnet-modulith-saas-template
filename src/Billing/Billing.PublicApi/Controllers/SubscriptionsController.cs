

using Billing.Application.Commands.CreateSubscription;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Manages subscription lifecycle for billing.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Billing")]
[Authorize("AdminOnly")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new subscription for a user.
    /// </summary>
    /// <param name="command">The user and plan to subscribe.</param>
    /// <returns>The new subscription ID.</returns>
    /// <response code="200">Subscription created successfully.</response>
    /// <response code="400">Invalid request (empty user, missing plan).</response>
    [HttpPost]
    //[ProducesResponseType<IActionResult>(StatusCodes.Status200OK)]
    //[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.SubscriptionId);
        }
        return BadRequest(result.Error);
    }
}