using AutoMapper;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.InteractionService.Controllers;

[ApiController]
[Route("api/interactions")]
public class InteractionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IInteractionRepository _interactionRepository;
    private readonly VoteHandlerService _voteHandlerService;

    public InteractionsController(IMapper mapper, IInteractionRepository interactionRepository, VoteHandlerService voteHandlerService)
    {
        _mapper = mapper;
        _interactionRepository = interactionRepository;
        _voteHandlerService = voteHandlerService;
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
    // this api handle vote only, because comment will be handle with message bus
    public async Task<IActionResult> HandleInteraction(UserInteractionDto userInteractionDto)
    {
        await _voteHandlerService.HandleVoteAsync(userInteractionDto);
        
        return Ok();
    }
}