using Application.Contracts.Posts;
using Application.Interfaces;

namespace Application.Services
{
    public class PostService(IJsonPlaceholderService jsonPlaceholderService) : IPostService
    {
        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            return await jsonPlaceholderService.CreatePostAsync(request);
        }

        public async Task<List<PostResponse>> GetAllAsync()
        {
            return [.. (await jsonPlaceholderService.GetPostsAsync())];
        }
    }
}
