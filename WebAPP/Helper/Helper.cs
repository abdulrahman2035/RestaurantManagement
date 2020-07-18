using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPP.Helper
{
    public class UserAPI
    {
        public static HttpClient Initail()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44341/");
            return Client;
        }

    }
}
