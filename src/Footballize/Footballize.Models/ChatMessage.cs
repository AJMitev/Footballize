namespace Footballize.Models
{
    using Abstracts;

    public class ChatMessage : BaseModel<string>
    {
        public string Sender { get; set; }
        public string GatherId { get; set; }
        public Gather Gather { get; set; }
        public string Text { get; set; }
    }
}
