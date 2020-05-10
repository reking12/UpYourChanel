using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.ViewModels.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsNew { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PostId { get; set; }
    }
}
