namespace Footballize.Models
{
    using System;
    using Abstracts;

    public class ChatMessage : BaseModel<string>
    {
        public ChatMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Sender { get; set; }
        public string GatherId { get; set; }
        public Gather Gather { get; set; }
        public string Text { get; set; }
    }
}
