using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChanel.Web.Models
{
    public class Word
    {
        public Word()
        {
            Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }

        public string SearchingWord { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
                      