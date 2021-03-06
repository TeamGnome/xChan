﻿using LibChan.Json;
using LibChan.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibChan.InfiniteChan
{
    public class InfiniteChanThreadPost : IChanThreadPost
    {
        [JsonProperty(PropertyName = "no")]
        public long PostId { get; set; }


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

        [JsonProperty(PropertyName = "extra_files")]
        public InfiniteChanPostImage[] ExtraFiles { get; set; }

        public ChanThreadPost GetViewModel(string slug, int threadId)
        {
            List<ChanPostFile> files = new List<ChanPostFile>();

            if(!string.IsNullOrEmpty(FileName))
            {
                files.Add(new ChanPostFile()
                {
                    Uri = string.Format("https://8ch.net/{0}/src/{1}{2}", slug, FileId, FileExtension),
                    ThumbnailUri = string.Format("https://8ch.net/{0}/thumb/{1}.jpg", slug, FileId),
                    Name = FileId,
                    Original = FileName,
                    Extension = FileExtension,
                    Height = FileHeight,
                    Width = FileWidth,
                    Size = FileSize,
                    MD5 = FileMd5
                });
            }

            if(ExtraFiles != null)
            {
                files.AddRange(from f in ExtraFiles
                               select new ChanPostFile()
                               {
                                   Uri = string.Format("https://8ch.net/{0}/src/{1}{2}", slug, f.FileId, f.FileExtension),
                                   ThumbnailUri = string.Format("https://8ch.net/{0}/thumb/{1}.jpg", slug, f.FileId),
                                   Name = f.FileId,
                                   Original = f.FileName,
                                   Extension = f.FileExtension,
                                   Height = f.FileHeight,
                                   Width = f.FileWidth,
                                   Size = f.FileSize,
                                   MD5 = f.FileMd5
                               });
            }

            return new ChanThreadPost()
            {
                BoardSlug = slug,
                ThreadId = threadId,
                PostId = PostId,

                Subject = Subject,
                Name = Name,
                Content = Content,

                TimeCreated = TimeCreated,

                Files = files.ToArray()
            };
        }
    }

    public class InfiniteChanPostImage
    {
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
    }

    public class InfiniteChanThreadPostObject
    {
        [JsonProperty(PropertyName = "posts")]
        public InfiniteChanThreadPost[] Posts { get; set; }
    }
}
