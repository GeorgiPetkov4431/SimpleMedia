namespace SimpleMedia.Web.ViewModels.Post
{
    public class PostAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public int LikesCount { get; set; }

        public string TagName { get; set; } = null!;
    }
}
