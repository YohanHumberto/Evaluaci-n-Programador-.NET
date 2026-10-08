namespace Application.Contracts.Posts
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}
