/*
 * This file is subject to the terms and conditions defined in
 * file 'license.txt', which is part of this source code package.
 */

using System;
using System.Net.Http;

namespace SteamKit2.CDN
{
    public partial class Client
    {
        /// <summary>
        /// When set to true, will attempt to download from a Lancache instance on the LAN rather than going out to Steam's CDNs.
        /// </summary>
        public static bool UseLancacheServer { get; set; }

        static HttpRequestMessage BuildLancacheRequest( Server server, string command, string? query)
        {
            var builder = new UriBuilder()
            {
                Scheme = "http",
                Host = "lancache.steamcontent.com",
                Port = 80,
                Path = command,
                Query = query ?? string.Empty
            };

            var request = new HttpRequestMessage( HttpMethod.Get, builder.Uri );
            if ( UseLancacheServer )
            {
                request.Headers.Host = server.Host;
                // User agent must match the Steam client in order for Lancache to correctly identify and cache Valve's CDN content
                request.Headers.Add( "User-Agent", "Valve/Steam HTTP Client 1.0" );
            }

            return request;
        }
    }
}
