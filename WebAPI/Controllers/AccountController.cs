using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        CustomjsonResult customjsonResult = new CustomjsonResult();
        LoginCustomjsonResult loginCustomjsonResult = new LoginCustomjsonResult();
        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        // POST: api/Account
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel Input)
        {
            
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                string tokenString = string.Empty;             
                tokenString = BuildToken();             
                loginCustomjsonResult.Result = "Sucessfully Login";
                loginCustomjsonResult.Email = Input.Email;
                loginCustomjsonResult.Token = tokenString;
                return Ok(loginCustomjsonResult);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                customjsonResult.Result = "Invalid login attempt";
                return Ok(customjsonResult);
            }

        }

       
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
       
        public async Task<ActionResult> Register(RegisterViewModel Input)
        {
            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                PhoneNumber=Input.Mobile,
                EmailConfirmed=true
                
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                customjsonResult.Result = "Error";
                return Ok(customjsonResult);
            }
            customjsonResult.Result = "Sucessfully Register";
            return Ok(customjsonResult);
        }


        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtToken:Issuer"],
              _config["JwtToken:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
