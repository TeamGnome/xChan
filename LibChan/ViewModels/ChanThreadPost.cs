using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChan.ViewModels
{
    public class ChanThreadPost
    {
        public string BoardSlug { get; set; }
        public long ThreadId { get; set; }
        public long PostId { get; set; }

        public string Subject { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime TimeCreated { get; set; }

        public ChanPostFile[] Files { get; set; }

        public string CreatedString
        {
            get
            {
                return TimeCreated.Humanize();
            }
        }
    }

    public class ChanPostFile
    {
        public string FileUri { get; set; }
        public string ThumbnailUri { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int FileHeight { get; set; }
        public int FileWidth { get; set; }
        public long FileSize { get; set; }
    }

    public interface IChanThreadPost
    {
        ChanThreadPost GetViewModel(string slug, int threadId);
    }
}
