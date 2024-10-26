using AutoMapper;
using InteractionMicroservice.Models.Dtos;
using InteractionMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace InteractionMicroservice.Controllers;

[ApiController]
[Route("api/interactions")]
public class InteractionsController : ControllerBase
{
    private readonly InteractionService _interactionService;

    public InteractionsController(InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [HttpPost]
    // this api handle user upvote/downvote interaction only
    // user comment interaction will automatically be handle with message bus consumer
    public async Task<IActionResult> PerformInteraction(InteractionDto request)
    {
        await _interactionService.HandleInteractionAsync(request);

        return NoContent();
    }
}