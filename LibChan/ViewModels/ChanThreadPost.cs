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
        public string Uri { get; set; }
        public string ThumbnailUri { get; set; }
        public string Name { get; set; }
        public string Original { get; set; }
        public string Extension { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public long Size { get; set; }
        public byte[] MD5 { get; set; }

        public string SizeWithUnit
        {
            get
            {
                var humanised = Size.Bytes();
                return string.Format("{0:F2}{1}", humanised.LargestWholeNumberValue, humanised.LargestWholeNumberSymbol);
            }
        }
        public string MD5String
        {
            get { return string.Join("", MD5.Select(h => h.ToString("X2"))).ToLower(); }
        }
    }

    public interface IChanThreadPost
    {
        ChanThreadPost GetViewModel(string slug, int threadId);
    }
}
