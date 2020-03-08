using System.Collections.Generic;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> TagsForWord(string word);
        void AddWordWithTagsInDataBase();
    }
}
