using Microsoft.AspNetCore.Mvc;
using InteractionMicroservice.Models.Dtos;
using InteractionMicroservice.Services;

namespace InteractionMicroservice.Controllers;

[ApiController]
[Route("api/interactions")]
public class InteractionsController(InteractionService interactionService) : ControllerBase
{
    [HttpPost]
    // this api handle user upvote/downvote interaction only
    // user comment interaction will automatically be handle with message bus consumer
    public async Task<IActionResult> PerformInteraction(InteractionDto request)
    {
        await interactionService.HandleInteractionAsync(request);

        return NoContent();
    }
}