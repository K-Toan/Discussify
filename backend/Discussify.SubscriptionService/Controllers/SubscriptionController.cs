using AutoMapper;
using Discussify.SubscriptionService.Interfaces;
using Discussify.SubscriptionService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.SubscriptionService.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var subscriptions = await _subscriptionService.GetAllAsync();
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var subscription = await _subscriptionService.GetByIdAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        return Ok(subscription);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserIdAsync(string userId)
    {
        var subscriptions = await _subscriptionService.GetByUserIdAsync(userId);
        return Ok(subscriptions);
    }

    [HttpGet("community/{communityId}")]
    public async Task<IActionResult> GetByCommunityIdAsync(int communityId)
    {
        var subscriptions = await _subscriptionService.GetByCommunityIdAsync(communityId);
        return Ok(subscriptions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SubscriptionCreateDto subscriptionCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdSubscription = await _subscriptionService.CreateAsync(subscriptionCreateDto);
        return Ok(createdSubscription);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _subscriptionService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}