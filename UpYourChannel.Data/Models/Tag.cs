namespace UpYourChannel.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public int WordId { get; set; }

        public virtual Word Word { get; set; }
    }
}