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
        // POST: api/account
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
            // Token Generation is not implemented yet.
            var tokenDto = new TokenDto()
            {
                Email = user.Email,
                Token = jwtService.CreatToken(user)
            };
            return Ok(tokenDto);
        }
    }
}
