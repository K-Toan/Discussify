using Discussify.PostService.Interfaces;
using Discussify.PostService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.PostService.Controllers;

[ApiController]
[Route("api/communities")]
public class CommunitiesController : ControllerBase
{
    private readonly ICommunityRepository _communityRepository;

    public CommunitiesController(ICommunityRepository communityRepository)
    {
        _communityRepository = communityRepository;
    }

    // GET: api/communities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Community>>> GetCommunities()
    {
        var communities = await _communityRepository.GetAsync();
        return Ok(communities);
    }

    // GET: api/communities/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Community>> GetCommunity(int communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        if (community == null)
        {
            return NotFound();
        }
        return Ok(community);
    }

    // POST: api/communities
    [HttpPost]
    public async Task<ActionResult<Community>> CreateCommunity([FromBody] Community community)
    {
        if (community == null)
        {
            return BadRequest("Community is null.");
        }

        await _communityRepository.AddAsync(community);
        return CreatedAtAction(nameof(GetCommunity), new { communityId = community.CommunityId }, community);
    }

    // PUT: api/communities/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCommunity(int id, [FromBody] Community community)
    {
        if (id != community.CommunityId)
        {
            return BadRequest("Community ID mismatch.");
        }

        var existingCommunity = await _communityRepository.GetByIdAsync(id);
        if (existingCommunity == null)
        {
            return NotFound();
        }

        await _communityRepository.UpdateAsync(community);
        return NoContent();
    }

    // DELETE: api/communities/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCommunity(int id)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null)
        {
            return NotFound();
        }

        await _communityRepository.DeleteAsync(id);
        return NoContent();
    }
}
