namespace Footballize.Models
{
    using System;
    using Abstracts;

    public class ProfilePicture : BaseDeletableModel<string>
    {
        public ProfilePicture()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string ContentType { get; set; }
        public string PathToFile { get; set; }
        public long Size { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}