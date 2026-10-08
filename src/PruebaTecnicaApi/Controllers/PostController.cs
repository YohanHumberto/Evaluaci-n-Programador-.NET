using Application.Contracts.Posts;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostController(
         IValidator<CreatePostRequest> validator,
         IPostService postService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postService.GetAllAsync();

            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequest request)
        {
            var validation = await validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var post = await postService.CreatePostAsync(request);

            return Ok(post);
        }
    }
}
