using Application.Contracts.Posts;

namespace Application.Interfaces
{
    public interface IJsonPlaceholderService
    {
        Task<IEnumerable<PostResponse>> GetPostsAsync();
        Task<PostResponse> CreatePostAsync(CreatePostRequest request);
    }
}
