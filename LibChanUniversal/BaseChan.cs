using LibChan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibChan
{
   /// <summary>
   /// This class represents as base Chan site that other sites will derive from.
   /// </summary>
   public abstract class BaseChan
   {
      public virtual string DisplayName => "xChan";
      public virtual string LogoUri => "";

      public virtual string JsonBoards => "/boards.json";
      public virtual string JsonCatalog => "/{0}/catalog.json";
      public virtual string JsonThread => "/{0}/thread/{1}.json";

      public virtual string WebThread => "/{0}/thread/{1}";

      public virtual BoardSorting DefaultBoardSorting { get { return BoardSorting.Alphabetical; } }

      public virtual IEnumerable<ChanBoard> GetBoards(BoardSorting? Sorting = null)
      {
         Task<IEnumerable<ChanBoard>> t = GetBoardsAsync(Sorting: Sorting ?? DefaultBoardSorting);
         t.Wait();
         return t.Result;
      }

      public abstract Task<IEnumerable<ChanBoard>> GetBoardsAsync(BoardSorting? Sorting = null);

      public virtual IEnumerable<ChanCatalogThread> GetCatalogForBoard(string urlSlug)
      {
         Task<IEnumerable<ChanCatalogThread>> t = GetCatalogForBoardAsync(urlSlug);
         t.Wait();
         return t.Result;
      }

      public abstract Task<IEnumerable<ChanCatalogThread>> GetCatalogForBoardAsync(string urlSlug);

      public virtual IEnumerable<ChanThreadPost> GetPostsForThead(string urlSlug, int threadId)
      {
         Task<IEnumerable<ChanThreadPost>> t = GetPostsForTheadAsync(urlSlug, threadId);
         t.Wait();
         return t.Result;
      }

      public abstract Task<IEnumerable<ChanThreadPost>> GetPostsForTheadAsync(string urlSlug, int threadId);
   }

   public enum BoardSorting
   {
      Alphabetical,
      MostPopular
   }
}
