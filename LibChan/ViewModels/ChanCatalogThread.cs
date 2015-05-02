using System;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Humanizer;

namespace LibChan.ViewModels
{
    public class ChanCatalogThread
    {
        public string BoardSlug { get; set; }
        public long ThreadId { get; set; }

        public string Subject { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime TimeCreated { get; set; }
        public DateTime TimeLastPost { get; set; }

        public int Posts { get; set; }
        public int Files { get; set; }

        public string ThumbnailUri { get; set; }


        public string ContentSafe
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                {
                    return "";
                }

                string brokenContent = Regex.Replace(Content, "<br ?/?>", Environment.NewLine);

                var node = HtmlNode.CreateNode("<p>" + brokenContent + "</p>");
                return WebUtility.HtmlDecode(node.InnerText);
            }
        }

        public string SubjectSafe
        {
            get
            {
                return WebUtility.HtmlDecode(Subject);
            }
        }

        public string CreatedString
        {
            get
            {
                return TimeCreated.Humanize();
            }
        }

        public string PostedString
        {
            get
            {
                return TimeLastPost.Humanize();
            }
        }

        public ChanThreadPost GetViewModel()
        {
            throw new NotImplementedException();
        }
    }

    public interface IChanCatalogThread
    {
        ChanCatalogThread GetViewModel(string slug);
    }
}
