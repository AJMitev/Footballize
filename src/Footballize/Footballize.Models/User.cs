namespace Footballize.Models
{
    using System.Collections.Generic;

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBanned { get; set; }
        public ICollection<User> Playpals { get; set; }
        public ICollection<Event> PlayedGames { get; set; }
    }
}
