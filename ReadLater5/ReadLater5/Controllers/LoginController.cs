using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReadLater5.Models;
using Services;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(IConfiguration config, ITokenService tokenService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _config = config;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest();
            }
            
            IdentityUser user = await _userManager.FindByEmailAsync(userLogin.UserName);
            var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, false,false);
            
            IActionResult response = Unauthorized();
            if (result.Succeeded)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), user);
                if (generatedToken != null)
                {
                    return Ok(generatedToken);
                }
                else
                {
                    return response;
                }
            }
            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            string message = "An error ...";
            return Ok(message);
        }
    }
}
