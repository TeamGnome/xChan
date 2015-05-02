using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using LibChan.ViewModels;

namespace LibChan.Archiving
{
    public static class DownloadManager
    {
        static DownloadManager()
        {
        }
    }

    public class DownloadParams
    {
        public BaseChan ChanProvider { get; set; }
        public ChanBoard Board { get; set; }
        public ChanCatalogThread Thread { get; set; }
    }
}
