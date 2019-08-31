namespace Footballize.Models
{
    using Abstracts;

    public class ProfilePicture : BaseDeletableModel<string>
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string PathToFile { get; set; }
        public long Size { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}