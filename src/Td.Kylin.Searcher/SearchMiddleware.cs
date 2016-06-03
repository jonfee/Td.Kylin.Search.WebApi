using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Td.Kylin.Searcher
{
    internal sealed class SearchMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string _partnerID;

        private readonly string _secret;
        
        private readonly List<ApiConfig> _configs;

        public SearchMiddleware(RequestDelegate next, string partnerID, string secret, List<ApiConfig> configs)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (configs == null)
            {
                throw new ArgumentNullException(nameof(configs));
            }
            _partnerID = partnerID;
            _secret = secret;
            _configs = configs;
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            ApiConfigRoot.PartnerID = _partnerID;
            ApiConfigRoot.Secret = _secret;
            ApiConfigRoot.Configs = _configs;

            return _next(context);
        }

        public SearchMiddleware(string partnerID, string secret,List<ApiConfig> configs)
        {
            if (configs == null)
            {
                throw new ArgumentNullException(nameof(configs));
            }

            ApiConfigRoot.PartnerID = partnerID;
            ApiConfigRoot.Secret = secret;
            ApiConfigRoot.Configs = configs;
        }
    }
}
