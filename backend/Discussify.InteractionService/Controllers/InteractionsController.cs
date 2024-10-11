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
        var existingInteraction = await _interactionRepository.GetInteractionByUserAndTargetId(request.UserId, request.TargetId);

        // already interacted
        if (existingInteraction != null)
        {
            // if interaction is upvote/downvote
            if(existingInteraction.Type != InteractionType.Comment)
            {
                if(existingInteraction.Type.Equals(request.Type))
                {
                    // remove interaction
                    await _interactionRepository.DeleteAsync(existingInteraction.InteractionId);
                    return Ok();
                }
                else
                {
                    // update new interaction
                    await _interactionRepository.UpdateAsync(_mapper.Map<UserInteraction>(request));
                    return Ok();
                }
            }
                
            // if interaction is comment, just skip
        }

        var userInteraction = _mapper.Map<UserInteraction>(request);

        userInteraction.CreateAt = DateTime.UtcNow;
        await _interactionRepository.AddAsync(userInteraction);

        return Ok(userInteraction);
    }
}