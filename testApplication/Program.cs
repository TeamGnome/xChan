using System;
using System.Linq;
using System.Threading.Tasks;
using LibChan;
using LibChan.FourChan;
using LibChan.InfiniteChan;
using LibChan.ViewModels;

namespace testApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var boards = ChanManager.ChanWebsites.SelectMany(c => c.GetCatalogForBoard("b"));

            Parallel.ForEach<ChanCatalogThread>(boards, (ChanCatalogThread t) =>
            {
                Console.WriteLine("{0}: {1:D3} posts, {2:D3} files", t.ThreadId, t.Posts, t.Files);
            });

            Console.WriteLine("TOTAL: {0} threads, {1} posts, {2} files", boards.Count(), boards.Sum(t => t.Posts), boards.Sum(t => t.Files));

            Console.ReadKey();
        }
    }
}
