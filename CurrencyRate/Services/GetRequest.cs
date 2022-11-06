using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CurrencyRate.Services
{
    public class GetRequest
    {
        private HttpWebRequest _request;
        private string _address;
        private string Responce { get; set; }
        public GetRequest(string address)
        {
            _address = address;
        }
        public string RunRequest()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream != null) Responce = new StreamReader(stream).ReadToEnd();
            return Responce;
        }

    }
}
