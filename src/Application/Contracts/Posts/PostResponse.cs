namespace Application.Contracts.Posts
{
    public class PostResponse
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
