using Application.Contracts.Posts;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<PostResponse> CreatePostAsync(CreatePostRequest request);
        Task<List<PostResponse>> GetAllAsync();
    }
}
