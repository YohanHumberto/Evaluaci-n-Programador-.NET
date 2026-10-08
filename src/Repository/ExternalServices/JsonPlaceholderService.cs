using Application.Contracts.Posts;
using Application.Interfaces;
using System.Net.Http.Json;

namespace Repository.ExternalServices
{
    public class JsonPlaceholderService(HttpClient httpClient) : IJsonPlaceholderService
    {
        public async Task<IEnumerable<PostResponse>> GetPostsAsync()
        {
            var response = await httpClient.GetAsync("posts");

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<IEnumerable<PostResponse>>()
                ?? [];
        }

        public async Task<PostResponse> CreatePostAsync(
            CreatePostRequest request)
        {
            var response = await httpClient.PostAsJsonAsync(
                "posts",
                request);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<PostResponse>()
                ?? throw new InvalidOperationException(
                    "No se pudo obtener la respuesta.");
        }
    }
}
