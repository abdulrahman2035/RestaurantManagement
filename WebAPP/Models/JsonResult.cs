using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Models
{
    public class JsonResultViewModel
    {
 
            public string result { get; set; }

    }

    public class LoginCustomjsonResult
    {
        public string Result { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
