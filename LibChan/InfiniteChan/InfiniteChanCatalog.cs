using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibChan.Json;
using LibChan.ViewModels;
using Newtonsoft.Json;

namespace LibChan.InfiniteChan
{
    public class InfiniteChanCatalogThread : IChanCatalogThread
    {
        [JsonProperty(PropertyName = "no")]
        public long ThreadId { get; set; }


        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "com")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        [JsonProperty(PropertyName = "time"), JsonConverter(typeof(UnixTimeConverter))]
        public DateTime TimeCreated { get; set; }


        [JsonProperty(PropertyName = "omitted_posts")]
        public int OmittedPosts { get; set; }

        [JsonProperty(PropertyName = "omitted_images")]
        public int OmittedFiles { get; set; }


        [JsonProperty(PropertyName = "replies")]
        public int Replies { get; set; }

        [JsonProperty(PropertyName = "images")]
        public int Files { get; set; }


        [JsonProperty(PropertyName = "sticky")]
        public bool Sticky { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }


        [JsonProperty(PropertyName = "last_modified"), JsonConverter(typeof(UnixTimeConverter))]
        public DateTime LastModified { get; set; }


        [JsonProperty(PropertyName = "tn_h")]
        public int ThumbnailHeight { get; set; }

        [JsonProperty(PropertyName = "tn_w")]
        public int ThumbnailWidth { get; set; }


        [JsonProperty(PropertyName = "h")]
        public int FileHeight { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int FileWidth { get; set; }

        [JsonProperty(PropertyName = "fsize")]
        public int FileSize { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "tim")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "md5")]
        public byte[] FileMd5 { get; set; }


        [JsonProperty(PropertyName = "resto")]
        public int ReplyTo { get; set; }

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
                TimeLastPost = LastModified,

                Posts = Replies + 1,
                Files = Files + OmittedFiles,

                ThumbnailUri = string.Format("https://8ch.net/{0}/thumb/{1}.jpg", slug, FileId)
            };
        }
    }

    public class InfiniteChanCatalogPage
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "threads")]
        public InfiniteChanCatalogThread[] Threads { get; set; }
    }
}
