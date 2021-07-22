//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TuneGenieParser
{
    using System;
    using System.Net;
    using System.Web;
    using Newtonsoft.Json;

    /// <summary>
    /// TuneGenie parser
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// User agent string to use for web requests
        /// </summary>
        private const string UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";

        /// <summary>
        /// Base url for web requests
        /// </summary>
        private readonly string baseUrl;

        /// <summary>
        /// Initializes a new instance of the Parser class
        /// </summary>
        /// <param name="callSign">Radio station call sign</param>
        public Parser(string callSign)
        {
            this.baseUrl = $"http://{callSign}.tunegenie.com";
        }

        /// <summary>
        /// Gets the list of songs played in the last hour
        /// </summary>
        /// <returns>List of songs</returns>
        public NowPlaying GetLastHour()
        {
            return this.GetLastHour(out _);
        }

        /// <summary>
        /// Gets the list of songs played in the last hour
        /// </summary>
        /// <param name="response">JSON response</param>
        /// <returns>List of songs</returns>
        public NowPlaying GetLastHour(out string response)
        {
            return this.GetNowPlaying(DateTime.Now.AddHours(-1), DateTime.Now, out response);
        }

        /// <summary>
        /// Gets the list of songs played in the last hour
        /// </summary>
        /// <returns>List of songs</returns>
        public NowPlaying GetLastDay()
        {
            return this.GetLastDay(out _);
        }

        /// <summary>
        /// Gets the list of songs played in the last hour
        /// </summary>
        /// <param name="response">JSON response</param>
        /// <returns>List of songs</returns>
        public NowPlaying GetLastDay(out string response)
        {
            return this.GetNowPlaying(DateTime.Now.AddDays(-1), DateTime.Now, out response);
        }

        /// <summary>
        /// Gets the list of songs played during the given period
        /// </summary>
        /// <param name="since">Starting period</param>
        /// <param name="until">Ending period</param>
        /// <param name="response">JSON response</param>
        /// <returns>List of songs</returns>
        public NowPlaying GetNowPlaying(DateTime since, DateTime until, out string response)
        {
            string sinceString = HttpUtility.UrlEncode(since.ToString("o"));
            string untilString = HttpUtility.UrlEncode(until.ToString("o"));
            string url = $"{this.baseUrl}/api/v1/brand/nowplaying/?since={sinceString}&until={untilString}";

            response = this.CreateWebClient().DownloadString(url);
            return this.Deserialize<NowPlaying>(response);
        }

        /// <summary>
        /// Deserializes a JSON response
        /// </summary>
        /// <typeparam name="T">Return data type</typeparam>
        /// <param name="response">JSON to deserialize</param>
        /// <returns>Deserialized data</returns>
        private T Deserialize<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }

        /// <summary>
        /// Creates a WebClient used to make web requests
        /// </summary>
        /// <returns>WebClient object</returns>
        private WebClient CreateWebClient()
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Referer", $"{this.baseUrl}/onair/");
            wc.Headers.Add("X-Requested-With", "XMLHttpRequest");
            wc.Headers[HttpRequestHeader.UserAgent] = UserAgent;
            return wc;
        }
    }
}
