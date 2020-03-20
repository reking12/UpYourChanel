using System.Collections.Generic;

namespace UpYourChannel.Data.Models
{
    public class Word
    {
        public Word()
        {
            Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }

        public string SearchingWord { get; set; }

        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}
                      