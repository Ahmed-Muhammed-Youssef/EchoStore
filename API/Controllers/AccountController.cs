﻿using API.DTOs;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly ICookieService _cookieService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ICookieService cookieService,
            IMapper mapper, SignInManager<AppUser> signInManager)
        {
            this._cookieService = cookieService;
            this._mapper = mapper;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        // POST: api/account/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return BadRequest("Used Email");
            }
            user = new AppUser()
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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                _cookieService.GetPrincipal(user, CookieAuthenticationDefaults.AuthenticationScheme));
            return Ok();
        }
        // GET: api/account
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindUserWithAddressByEmail(email);
            if (user == null)
            {
                return BadRequest();
            }
            var userDto = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                UserName = user.UserName,
                Address = _mapper.Map<Address, AddressDto>(user.Address)
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
            if (signedInUser.Email != user.Email)
            {
                return BadRequest();
            }
            // update the app user data
            signedInUser.DisplayName = user.DisplayName;
            // update the app user address
            signedInUser.DisplayName = user.DisplayName;
            signedInUser.Address = _mapper.Map<AddressDto, Address>(user.Address);

            var res = await _userManager.UpdateAsync(signedInUser);

            if (!res.Succeeded)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        // POST: api/account/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(loginDto);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(loginDto);
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                _cookieService.GetPrincipal(user, CookieAuthenticationDefaults.AuthenticationScheme));
            return Ok();
        }
        // POST: api/account/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
