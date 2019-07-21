namespace Footballize.Models.Abstracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public abstract class  BaseModel<TKey> : IAuditInfo
    {
        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public TKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}