using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibChan.ViewModels;
using System.Linq;

namespace LibChan.InfiniteChan
{
    public class InfiniteChanApi : BaseChan
    {
        public override string DisplayName { get { return "\u221EChan"; } }
        public override string LogoUri { get { return "http://upload.wikimedia.org/wikipedia/en/thumb/a/a5/InfiniteChan%E2%88%9EchanLogo.svg/315px-InfiniteChan%E2%88%9EchanLogo.svg.png"; } }

        public override string JsonBoards { get { return "https://8ch.net/boards.json"; } }
        public override string JsonCatalog { get { return "https://8ch.net/{0}/catalog.json"; } }
        public override string JsonThread { get { return "https://8ch.net/{0}/res/{1}.json"; } }

        public override string WebThread { get { return "https://8ch.net/{0}/res/{1}.html"; } }

        public override BoardSorting DefaultBoardSorting { get { return BoardSorting.MostPopular; } }

        public override async Task<IEnumerable<ChanBoard>> GetBoardsAsync(BoardSorting? Sorting = null)
        {
            var data = await ApiCache.GetApi<InfiniteChanBoard[]>(JsonBoards);

            IEnumerable<InfiniteChanBoard> boards = data;

            switch (Sorting ?? DefaultBoardSorting)
            {
                case BoardSorting.Alphabetical:
                    boards = boards.OrderBy(b => b.Uri);
                    break;
                case BoardSorting.MostPopular:
                    boards = boards.OrderByDescending(b => b.PostsPerDay);
                    break;
            }

            return from b in boards
                   select b.GetViewModel();
        }

        public override async Task<IEnumerable<ChanCatalogThread>> GetCatalogForBoardAsync(string urlSlug)
        {
            var data = await ApiCache.GetApi<InfiniteChanCatalogPage[]>(string.Format(JsonCatalog, urlSlug));

            return from t in data.SelectMany(p => p.Threads)
                   select t.GetViewModel(urlSlug);
        }

        public override async Task<IEnumerable<ChanThreadPost>> GetPostsForTheadAsync(string urlSlug, int threadId)
        {
            var data = await ApiCache.GetApi<InfiniteChanThreadPostObject>(string.Format(JsonThread, urlSlug, threadId));

            return data.Posts.Select(p => p.GetViewModel(urlSlug, threadId));
        }
    }
}
