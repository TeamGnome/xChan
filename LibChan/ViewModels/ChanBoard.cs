using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChan.ViewModels
{
    public class ChanBoard
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string UrlSlug { get; set; }

        public bool IsNsfw { get; set; }
    }

    public interface IChanBoard
    {
        ChanBoard GetViewModel();
    }
}
