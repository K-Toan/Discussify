using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;
using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Application.Services;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetPostsAsync(int pageIndex, int pageSize, string orderBy);
    Task<PostDto?> GetPostByIdAsync(int postId);
    Task<Post> CreatePostAsync(CreatePostDto createPostDto);
    Task UpdatePostAsync(UpdatePostDto updatePostDto);
    Task DeletePostAsync(int postId);
}
