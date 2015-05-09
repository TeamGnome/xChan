using LibChan.Json;
using LibChan.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibChan.FourChan
{
    public class FourChanThreadPost : IChanThreadPost
    {
        /*
            Some properties are ignored here.
            "now" - posted date/time string. We just calculate it from the unix timestamp instead. More accurate, less hassle.
        */
        [JsonProperty(PropertyName = "no")]
        public int PostNumber { get; set; }

        [JsonProperty(PropertyName = "resto")]
        public int RepliedTo { get; set; }

        [JsonProperty(PropertyName = "sticky")]
        public bool IsSticky { get; set; }

        [JsonProperty(PropertyName = "closed")]
        public bool IsClosed { get; set; }

        public DateTime Created
        {
            get { return new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(CreatedStamp)); }
        }

        [JsonProperty(PropertyName = "time")]
        public long CreatedStamp { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "trip")]
        public string Trip { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "capcode")]
        public string Capcode { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "com")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "tim")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string FileOriginalName { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string FileExtension { get; set; }

        [JsonProperty(PropertyName = "fsize")]
        public int FileSize { get; set; }

        [JsonProperty(PropertyName = "md5")]
        public byte[] FileMD5 { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int ImageWidth { get; set; }

        [JsonProperty(PropertyName = "h")]
        public int ImageHeight { get; set; }

        [JsonProperty(PropertyName = "tn_w")]
        public int ThumbWidth { get; set; }

        [JsonProperty(PropertyName = "tn_h")]
        public int ThumbHeight { get; set; }

        [JsonProperty(PropertyName = "filedeleted")]
        public bool IsFileDeleted { get; set; }

        [JsonProperty(PropertyName = "spoiler")]
        public bool IsSpoilerImage { get; set; }

        [JsonProperty(PropertyName = "custom_spoiler")]
        public int CustomSpoiler { get; set; }

        [JsonProperty(PropertyName = "omitted_posts")]
        public int OmittedPosts { get; set; }

        [JsonProperty(PropertyName = "omitted_images")]
        public int OmittedImages { get; set; }

        [JsonProperty(PropertyName = "replies")]
        public int ReplyCount { get; set; }

        [JsonProperty(PropertyName = "images")]
        public int FileCount { get; set; }

        [JsonProperty(PropertyName = "bumplimit")]
        public bool IsBumpLimitMet { get; set; }

        [JsonProperty(PropertyName = "imagelimit")]
        public bool IsFileLimitMet { get; set; }

        [JsonProperty(PropertyName = "capcode_replies")]
        public Dictionary<string, int[]> CapcomReplies { get; set; }

        [JsonProperty(PropertyName = "last_modified")]
        public long LastModifiedStamp { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string ThreadTag { get; set; }

        [JsonProperty(PropertyName = "semantic_url")]
        public string SemanticUrl { get; set; }

        [JsonProperty(PropertyName = "last_replies")]
        public FourChanThreadPost[] LastReplies { get; set; }

        public ChanThreadPost GetViewModel(string slug, int threadId)
        {
            List<ChanPostFile> files = new List<ChanPostFile>();

            if(!string.IsNullOrEmpty(FileName))
            {
                files.Add(new ChanPostFile()
                {
                    Uri = string.Format("https://i.4cdn.org/{0}/{1}{2}", slug, FileName, FileExtension),
                    ThumbnailUri = string.Format("https://i.4cdn.org/{0}/{1}s.jpg", slug, FileName),
                    Name = FileName,
                    Extension = FileExtension,
                    Height = ImageHeight,
                    Width = ImageWidth,
                    Size = FileSize,
                    MD5 = FileMD5
                });
            }

            return new ChanThreadPost()
            {
                BoardSlug = slug,
                ThreadId = threadId,
                PostId = PostNumber,

                Subject = Subject,
                Name = Name,
                Content = Comment,

                TimeCreated = Created,

                Files = files.ToArray()
            };
        }
    }

    public class FourChanThreadPostObject
    {
        [JsonProperty(PropertyName = "posts")]
        public FourChanThreadPost[] Posts { get; set; }
    }
}
