using LibChan.Json;
using LibChan.ViewModels;
using Newtonsoft.Json;
using System;

namespace LibChan.InfiniteChan
{
   public class InfiniteChanBoard : IChanBoard
   {
      [JsonProperty(PropertyName = "uri")]
      public string Uri { get; set; }

      [JsonProperty(PropertyName = "title")]
      public string Title { get; set; }

      [JsonProperty(PropertyName = "subtitle")]
      public string Subtitle { get; set; }


      [JsonProperty(PropertyName = "time")]
      public DateTime? Created { get; set; }

      [JsonProperty(PropertyName = "indexed"), JsonConverter(typeof(BoolConverter))]
      public bool IsIndexed { get; set; }

      [JsonProperty(PropertyName = "sfw"), JsonConverter(typeof(BoolConverter))]
      public bool IsSfw { get; set; }


      [JsonProperty(PropertyName = "pph")]
      public int PostsPerHour { get; set; }

      [JsonProperty(PropertyName = "ppd")]
      public int PostsPerDay { get; set; }

      [JsonProperty(PropertyName = "max")]
      public int? MaxPosts { get; set; }

      [JsonProperty(PropertyName = "uniq_ip")]
      public int? UniqueIPs { get; set; }


      [JsonProperty(PropertyName = "tags")]
      public string[] Tags { get; set; }


      [JsonProperty(PropertyName = "img")]
      public string Flag { get; set; }

      [JsonProperty(PropertyName = "ago")]
      public string CreatedString { get; set; }


      public ChanBoard GetViewModel() => new ChanBoard()
      {
         Title = Title,
         Subtitle = Subtitle,
         UrlSlug = Uri,
         IsNsfw = !IsSfw
      };
   }
}
