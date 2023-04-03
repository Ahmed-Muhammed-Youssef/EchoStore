using API.DTOs;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
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
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IJwtService jwtService, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }
        // GET: api/account
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault( c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindUserWithAddressByEmail(email);
            if(user == null)
            {
                return BadRequest();
            }
            var userDto = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                UserName = user.UserName,
                Address = mapper.Map<Address, AddressDto>(user.Address)
            };

            return Ok(userDto);
        }
        
        /// <summary>
        /// used to edit the current user data (only the address and display name)
        /// </summary>
        
        // PUT: api/account/edit
        [HttpPut("edit")]
        [Authorize]
        public async Task<ActionResult<UserDto>> EditCurrentUser(UserDto user)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var signedInUser = await _userManager.FindUserWithAddressByEmail(email);
            if (signedInUser == null)
            {
                return BadRequest();
            }
            if(signedInUser.Email != user.Email)
            {
                return BadRequest();
            }
            // update the app user data
            signedInUser.DisplayName = user.DisplayName;
            // update the app user address
            signedInUser.DisplayName = user.DisplayName;           
            signedInUser.Address = mapper.Map<AddressDto, Address>(user.Address);
           
            var res = await _userManager.UpdateAsync(signedInUser);
            
            if (!res.Succeeded)
            {
                return BadRequest();
            }
            return Ok(user);
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
