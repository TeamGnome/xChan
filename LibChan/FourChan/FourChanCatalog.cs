using LibChan.Json;
using LibChan.ViewModels;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace LibChan.FourChan
{
    public class FourChanCatalogThread : IChanCatalogThread
    {
        [JsonProperty(PropertyName = "no")]
        public long ThreadId { get; set; }


        [JsonProperty(PropertyName = "now")]
        public string TimeCreatedString { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "com")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int FileWidth { get; set; }

        [JsonProperty(PropertyName = "h")]
        public int FileHeight { get; set; }

        [JsonProperty(PropertyName = "tn_w")]
        public int FileThumbnailWidth { get; set; }

        [JsonProperty(PropertyName = "tn_h")]
        public int FileThumnailHeight { get; set; }

        [JsonProperty(PropertyName = "tim")]
        public long FileId { get; set; }


        [JsonProperty(PropertyName = "time"), JsonConverter(typeof(UnixTimeConverter))]
        public DateTime TimeCreated { get; set; }


        [JsonProperty(PropertyName = "md5")]
        public byte[] FileHash { get; set; }

        [JsonProperty(PropertyName = "fsize")]
        public int FileSize { get; set; }


        [JsonProperty(PropertyName = "resto")]
        public int ReplyTo { get; set; }


        [JsonProperty(PropertyName = "bumplimit")]
        public int BumpLimit { get; set; }

        [JsonProperty(PropertyName = "imagelimit")]
        public int FileLimit { get; set; }


        [JsonProperty(PropertyName = "semantic_url")]
        public string SemanticUrl { get; set; }


        [JsonProperty(PropertyName = "replies")]
        public int Replies { get; set; }

        [JsonProperty(PropertyName = "images")]
        public int Files { get; set; }


        [JsonProperty(PropertyName = "omitted_posts")]
        public int OmittedReplies { get; set; }

        [JsonProperty(PropertyName = "omitted_images")]
        public int OmittedFiles { get; set; }


        [JsonProperty(PropertyName = "last_replies")]
        public FourChanCatalogThreadReply[] LastReplies { get; set; }


        public ChanCatalogThread GetViewModel(string slug)
        {
            return new ChanCatalogThread()
            {
                BoardSlug = slug,
                ThreadId = ThreadId,

                Subject = Subject,
                Name = Name,
                Content = Content,

                TimeCreated = TimeCreated,
                TimeLastPost = LastReplies != null && LastReplies.Length > 0 ? LastReplies.Last().TimeCreated : TimeCreated,

                Posts = Replies + 1,
                Files = Files,

                ThumbnailUri = string.Format("https://t.4cdn.org/{0}/{1}s.jpg", slug, FileId)
            };
        }
    }

    public class FourChanCatalogThreadReply
    {
        [JsonProperty(PropertyName = "no")]
        public long PostId { get; set; }


        [JsonProperty(PropertyName = "now")]
        public string TimeCreatedString { get; set; }


        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "com")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int FileWidth { get; set; }

        [JsonProperty(PropertyName = "h")]
        public int FileHeight { get; set; }

        [JsonProperty(PropertyName = "tn_w")]
        public int FileThumbnailWidth { get; set; }

        [JsonProperty(PropertyName = "tn_h")]
        public int FileThumnailHeight { get; set; }

        [JsonProperty(PropertyName = "tim")]
        public long FileId { get; set; }


        [JsonProperty(PropertyName = "time"), JsonConverter(typeof(UnixTimeConverter))]
        public DateTime TimeCreated { get; set; }


        [JsonProperty(PropertyName = "md5")]
        public byte[] FileMd5 { get; set; }

        [JsonProperty(PropertyName = "fsize")]
        public int FileSize { get; set; }


        [JsonProperty(PropertyName = "resto")]
        public int ReplyTo { get; set; }

    }

    public class FourChanCatalogPage
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "threads")]
        public FourChanCatalogThread[] Threads { get; set; }
    }
}
