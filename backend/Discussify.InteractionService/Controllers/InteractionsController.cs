using AutoMapper;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.InteractionService.Controllers;

[ApiController]
[Route("api/interactions")]
public class InteractionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IInteractionRepository _interactionRepository;

    public InteractionsController(IMapper mapper, IInteractionRepository interactionRepository)
    {
        _mapper = mapper;
        _interactionRepository = interactionRepository;
    }

    [HttpGet]
    [Route("{targetId}")]
    public async Task<IActionResult> GetTargetInteraction(int targetId)
    {
        var targetInteractionCounts = await _interactionRepository.GetTargetInteractionCounts(targetId);
        if (targetInteractionCounts != null)
        {
            return Ok(targetInteractionCounts);
        }
        return BadRequest("Post/Comment not found");
    }

    [HttpPost]
    public async Task<IActionResult> PerformInteraction(UserInteractionCreateDto request)
    {
        var userInteraction = _mapper.Map<UserInteraction>(request);

        // user comment on post/other comment
        if (request.Type == InteractionType.Comment)
        {
            await _interactionRepository.AddAsync(userInteraction);

            return Ok(userInteraction);
        }
        // interaction is upvote/downvote
        else
        {
            var existingInteraction = await _interactionRepository.GetInteractionByUserAndTargetId(request.UserId, request.TargetId);

            // already existed a interaction before
            if (existingInteraction != null)
            {
                // if type is the same as old interaction
                if (existingInteraction.Type.Equals(request.Type))
                {
                    // remove old interaction
                    await _interactionRepository.DeleteAsync(existingInteraction.InteractionId);
                }
                else
                {
                    // update new interaction
                    existingInteraction.Type = request.Type;
                    await _interactionRepository.UpdateAsync(existingInteraction);
                }
                return Ok(existingInteraction);
            }
            else
            {
                await _interactionRepository.AddAsync(userInteraction);
            }
            return Ok(userInteraction);
        }
    }
}