namespace Footballize.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.GathersPlayed = new HashSet<GatherUser>();
            this.GamesRecruited = new HashSet<RecruitmentUser>();
            this.PlaypalsAdded = new HashSet<Playpal>();
            this.PlaypalsAddedMe = new HashSet<Playpal>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? BanUntil { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        
        public ICollection<GatherUser> GathersPlayed { get; set; }
        public ICollection<RecruitmentUser> GamesRecruited { get; set; }
        public ICollection<Playpal> PlaypalsAdded { get; set; }
        public ICollection<Playpal> PlaypalsAddedMe { get; set; }


        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
