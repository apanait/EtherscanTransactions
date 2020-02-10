using System;
using System.Collections.Generic;
using System.Text;

namespace UportTransactions.BLL.Services
{
    public class UrlBuilder
    {
        private string _module;
        private string _action;
        private string _fromBlock;
        private string _toBlock;
        private string _address;
        private string _apikey;
        private string _apiBase;

        public UrlBuilder(string apiBase)
        {
            _apiBase = apiBase;
        }

        public UrlBuilder WithModule(string module)
        {
            if(string.IsNullOrEmpty(module))
            {
                throw new ArgumentNullException(nameof(module));
            }

            _module = module;
            
            return this;
        }

        public UrlBuilder WithAction(string action)
        {
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException(nameof(action));
            }

            _action = action;

            return this;
        }

        public UrlBuilder WithFromBlock(string fromBlock)
        {
            if (string.IsNullOrEmpty(fromBlock))
            {
                throw new ArgumentNullException(nameof(fromBlock));
            }

            _fromBlock = fromBlock;

            return this;
        }
        public UrlBuilder WithToBlock(string toBlock)
        {
            if (string.IsNullOrEmpty(toBlock))
            {
                throw new ArgumentNullException(nameof(toBlock));
            }

            _toBlock = toBlock;

            return this;
        }
        public UrlBuilder WithAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            _address = address;

            return this;
        }
        public UrlBuilder WithApiKey(string apikey)
        {
            if (string.IsNullOrEmpty(apikey))
            {
                throw new ArgumentNullException(nameof(apikey));
            }

            _apikey = apikey;

            return this;
        }

        public Uri Build()
        {
            var sb = new StringBuilder(_apiBase);
            sb.AppendFormat($"?module={_module}");
            sb.AppendFormat($"&action={_action}");
            sb.AppendFormat($"&fromBlock={_fromBlock}");
            sb.AppendFormat($"&toBlock={_toBlock}");
            sb.AppendFormat($"&address={_address}");
            sb.AppendFormat($"&apikey={_apikey}");

            return new Uri(sb.ToString());
        }
    }
}

