using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using peace_api.Dtos.Account;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                AppUser? appUser = new()
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                IdentityResult? result = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (result.Succeeded)
                {
                    IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        NewUserDto? newUser = new()
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                        };

                        return Ok(newUser);
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AppUser? user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid username or password!" });
            }

            NewUserDto? loggedInUser = new()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                RefreshToken = await _tokenService.CreateToken(user, 7)
            };

            user.RefreshToken = loggedInUser.RefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(loggedInUser);
        }

        //POST: api/account/refresh-token
        [HttpPost("verify-token")]
        public async Task<ActionResult> VerifyToken([FromBody] RefreshTokenDto refreshTokenDto)
        {

            try
            {
                string? token = Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

                TokenValidationParameters? TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    ValidateAudience = true,
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false,

                };

                // validate the token
                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal? validatedToken = tokenHandler.ValidateToken(token, TokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken)
                    return Forbid();

                string? userId = jwtSecurityToken.Claims.First(c => c.Type == "userId").Value;


                AppUser? user = await _userManager.FindByIdAsync(userId);

                if (user == null || user.RefreshToken != refreshTokenDto.RefreshToken)
                {
                    return Forbid();
                }

                if (user.RefreshTokenExpiry < DateTime.UtcNow)
                {
                    return Forbid();
                }

                NewUserDto? loggedInUser = new()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user),
                    RefreshToken = await _tokenService.CreateToken(user, 7)
                };

                user.RefreshToken = loggedInUser.RefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);

                return Ok(loggedInUser);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        //GET:api/account/logout
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? userId = User.Claims.First(c => c.Type == "userId").Value;

            AppUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}