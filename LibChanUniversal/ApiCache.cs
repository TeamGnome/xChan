using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace LibChan
{
   public static class ApiCache
   {
      public static int SecondsBeforeStale { get; set; }

      private static Dictionary<string, ApiCacheEntry> _cache = new Dictionary<string, ApiCacheEntry>();

      static ApiCache()
      {
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
         ApiCacheEntry ret = new ApiCacheEntry();
         using (HttpRequestMessage hreq = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
         using (HttpClient hc = new HttpClient())
         using (HttpResponseMessage hres = await hc.SendRequestAsync(hreq))
         {
            ret.ExpiryTime = DateTime.Now.AddSeconds(SecondsBeforeStale);
            ret.LastModified = hres.Headers.ContainsKey("Last-Modified") ?
               DateTime.Parse(hres.Headers["Last-Modified"]) :
               DateTime.Now;

            using (IInputStream iis = await hres.Content.ReadAsInputStreamAsync())
            using (Stream s = iis.AsStreamForRead())
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
         using (HttpRequestMessage hreq = new HttpRequestMessage(HttpMethod.Head, new Uri(url)))
         using (HttpClient hc = new HttpClient())
         {
            hreq.Headers.Add("If-Modified-Since", lastChecked.ToUniversalTime().ToString("r"));
            using (HttpResponseMessage hres = await hc.SendRequestAsync(hreq))
            {
               return !(hres.StatusCode == HttpStatusCode.NotModified || hres.StatusCode == HttpStatusCode.NotFound);
            }
         }
      }
   }

   internal struct ApiCacheEntry
   {
      public DateTime ExpiryTime { get; set; }
      public DateTime LastModified { get; set; }
      public object Value { get; set; }
   }
}
