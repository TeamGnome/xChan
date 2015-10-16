using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibChan.FourChan;
using LibChan.InfiniteChan;

namespace LibChan
{
    public static class ChanManager
    {
        public static List<BaseChan> ChanWebsites { get; set; }

        static ChanManager()
        {
            ChanWebsites = new List<BaseChan>();

            ChanWebsites.Add(new FourChanApi());
            ChanWebsites.Add(new InfiniteChanApi());
        }
    }
}
