using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AccountViewModel
    {
       
    }
    public class LoginViewModel
    {       
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CustomjsonResult
    {
        public string Result { get; set; }
    }

    public class LoginCustomjsonResult
    {
        public string Result { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       // public string FullName { get; set; }       
        public string Mobile { get; set; }

        public bool EmailConfirmed { get; set; }



    }

  
}
