using API.DTOs;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService jwtService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IJwtService jwtService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.jwtService = jwtService;
        }
        // POST: api/account/register
        [HttpPost("register")]
        public async Task<ActionResult<TokenDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            var tokenDto = new TokenDto()
            {
                Email = user.Email,
                Token = jwtService.CreatToken(user)
            };
            return Ok(tokenDto);
        }
        // POST: api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody]LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
            {
                return Unauthorized(loginDto);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded)
            {
                return Unauthorized(loginDto);
            }
            var tokenDto = new TokenDto()
            {
                Email = user.Email,
                Token = jwtService.CreatToken(user)
            };
            return Ok(tokenDto);
        }
    }
}
