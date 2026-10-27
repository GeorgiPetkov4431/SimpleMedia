namespace SimpleMedia.Web.ViewModels.Post
{
    public class PostDeleteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
