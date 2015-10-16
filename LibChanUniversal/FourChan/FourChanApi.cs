using LibChan.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibChan.FourChan
{
   public class FourChanApi : BaseChan
   {
      public override string DisplayName => "4Chan";
      public override string LogoUri => "http://s.4cdn.org/image/fp/logo.png";

      public override string JsonBoards => "https://a.4cdn.org/boards.json";
      public override string JsonCatalog => "https://a.4cdn.org/{0}/catalog.json";
      public override string JsonThread => "https://a.4cdn.org/{0}/thread/{1}.json";

      public override string WebThread => "https://boards.4chan.org/{0}/thread/{1}";

      public override async Task<IEnumerable<ChanBoard>> GetBoardsAsync(BoardSorting? Sorting = null)
      {
         var data = await ApiCache.GetApi<FourChanBoards>(JsonBoards);

         IEnumerable<FourChanBoard> boards = data.Boards;

         switch (Sorting ?? DefaultBoardSorting)
         {
            case BoardSorting.Alphabetical:
            case BoardSorting.MostPopular:
               boards = boards.OrderBy(b => b.BoardName);
               break;
         }

         return from b in boards
                select b.GetViewModel();
      }

      public override async Task<IEnumerable<ChanCatalogThread>> GetCatalogForBoardAsync(string urlSlug)
      {
         var data = await ApiCache.GetApi<FourChanCatalogPage[]>(string.Format(JsonCatalog, urlSlug));

         return from t in data.SelectMany(p => p.Threads)
                select t.GetViewModel(urlSlug);
      }

      public override async Task<IEnumerable<ChanThreadPost>> GetPostsForTheadAsync(string urlSlug, int threadId)
      {
         var data = await ApiCache.GetApi<FourChanThreadPostObject>(string.Format(JsonThread, urlSlug, threadId));

         return data.Posts.Select(p => p.GetViewModel(urlSlug, threadId));
      }
   }
}
