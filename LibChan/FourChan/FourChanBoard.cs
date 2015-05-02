using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibChan.ViewModels;
using Newtonsoft.Json;

namespace LibChan.FourChan
{
    public class FourChanBoards
    {
        [JsonProperty(PropertyName = "boards")]
        public FourChanBoard[] Boards { get; set; }
    }

    public class FourChanBoard : IChanBoard
    {
        [JsonProperty(PropertyName = "board")]
        public string BoardName { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string BoardTitle { get; set; }

        [JsonProperty(PropertyName = "ws_board")]
        public bool IsNsfw { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int PostsPerPage { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        public ChanBoard GetViewModel()
        {
            return new ChanBoard()
            {
                Title = BoardTitle,
                Subtitle = "",
                UrlSlug = BoardName,
                IsNsfw = IsNsfw
            };
        }
    }
}
