using Microsoft.EntityFrameworkCore;
using RazorClient.Data;
using RazorClient.Models;

namespace RazorClient.Services
{
    public class OfflinePostService
    {
        private readonly OfflinePostDbContext _context;
        private readonly PostService _postService;

        public OfflinePostService(OfflinePostDbContext context, PostService postService)
        {
            _context = context;
            _postService = postService;
        }

        public async Task<List<OfflinePost>> GetOfflinePosts()
        {
            return await _context.OfflinePosts.ToListAsync();
        }

        public async Task SavePost(int postId)
        {
            var existedPost = _context.OfflinePosts.FirstOrDefault(post => post.Id == postId);
            if (existedPost != null)
            {
                return;
            }

            var post = await _postService.GetPostByIdAsync(postId);

            OfflinePost offlinePost = new OfflinePost
            {
                PostId = post.PostId,
                UserId = post.UserId,
                UserName = post.UserName,
                CommunityId = post.CommunityId,
                CommunityName = post.CommunityName,
                Title = post.Title,
                Content = post.Content,
                Upvote = post.Upvote,
                Downvote = post.Downvote,
                Comment = post.Comment,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };

            Console.WriteLine("Saving post " + offlinePost.ToString());

            _context.OfflinePosts.Add(offlinePost);
            _context.SaveChanges();
        }

        public async Task UnsavePost(int postId)
        {
            var post = await _context.OfflinePosts.FirstOrDefaultAsync(p => p.PostId == postId);

            if(post != null)
            {
                _context.OfflinePosts.Remove(post);
                _context.SaveChanges();
            }
        }
    }
}
