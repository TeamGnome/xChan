using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibChan
{
    public static class ApiCache
    {
        public static int SecondsBeforeStale { get; set; }

        private static Dictionary<string, ApiCacheEntry> _cache = new Dictionary<string, ApiCacheEntry>();

        static ApiCache()
        {
            ServicePointManager.DefaultConnectionLimit = 20;
            SecondsBeforeStale = 15;
        }

        /// <summary>
        /// Download and parse a JSON document into the specified type
        /// </summary>
        /// <typeparam name="T">Object that JSON Reperesents</typeparam>
        /// <param name="url">The URL that the JSON can be retreived from</param>
        /// <returns></returns>
        public async static Task<T> GetApi<T>(string url)
        {
            if (_cache.ContainsKey(url)
                && (_cache[url].ExpiryTime > DateTime.Now || !await IsAPIStale(url, _cache[url].LastModified)))
            {
                return (T)(_cache[url].Value);
            }
            else
            {
                var cat = await FetchAPI<T>(url);
                _cache[url] = cat;
                return (T)(cat.Value);
            }
        }

        private async static Task<ApiCacheEntry> FetchAPI<T>(string url)
        {
            HttpWebRequest wreq = HttpWebRequest.CreateHttp(url);
            ApiCacheEntry ret = new ApiCacheEntry();

            using (HttpWebResponse wres = await wreq.GetResponseAsync() as HttpWebResponse)
            {
                ret.ExpiryTime = DateTime.Now.AddSeconds(SecondsBeforeStale);
                ret.LastModified = wres.LastModified;

                using (Stream s = wres.GetResponseStream())
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader jr = new JsonTextReader(sr))
                {

                    JsonSerializer jsz = new JsonSerializer();

                    ret.Value = jsz.Deserialize<T>(jr);
                }
            }

            return ret;
        }

        private async static Task<bool> IsAPIStale(string url, DateTime lastChecked)
        {
            HttpWebRequest wreq = HttpWebRequest.CreateHttp(url);
            wreq.IfModifiedSince = lastChecked;
            wreq.Method = "HEAD";

            try
            {
                HttpWebResponse wres = await wreq.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException wex)
            {
                HttpWebResponse wres = wex.Response as HttpWebResponse;
                if (wres.StatusCode == HttpStatusCode.NotModified || wres.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
            }
            return true;
        }
    }

    internal struct ApiCacheEntry
    {
        public DateTime ExpiryTime { get; set; }
        public DateTime LastModified { get; set; }
        public object Value { get; set; }
    }
}
