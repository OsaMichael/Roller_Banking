using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Roller.DataContext.Entity;
using Roller.Repository.Extensions;
using Roller.Web.Models;

namespace Roller.Web.Areas.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger _logger;

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, IPasswordHasher<User> passwordHasher, ILogger<AuthController> logger)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        [HttpPost("login")]

        [ProducesResponseType(typeof(ApiResponse<UserForLoginViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
       // public async Task<IActionResult> Login([FromBody] UserForLoginViewModel model)
        public async Task<IActionResult> Login([FromBody] LoginViewModel user)
        {
            var userr = await _userManager.Users.SingleOrDefaultAsync(r => r.Email == user.Email);
            if (userr == null)
            {
                return BadRequest("Invalid client request");
            }

            if (_passwordHasher.VerifyHashedPassword(userr, userr.PasswordHash, user.Password) == PasswordVerificationResult.Success)
            {
                await _signInManager.SignInAsync(userr, user.RememberMe);
                var token = await GenerateJwtToken(user.Email, userr);

                return Ok(token);

            }
            return BadRequest("Validation Failed: {ModelState.ExtractErrorString()}");
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]));
            ////IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtKey"])),
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        }


        private async Task<AuthModel> GenerateJwtToken(string email, User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> role = await _userManager.GetRolesAsync(user);
            string roles = string.Empty;
            if (role.Any())
            {
                roles = role.Join();
            }

            var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, email),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.UserName),
                        new Claim("email", user.Email),
                        new Claim("username", user.UserName),
                        new Claim("roles",roles),
                      //  new Claim("companyName",user.CompanyName),


                    }.Union(userClaims);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]));
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtKey"])),
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        _config["Issuer"],
                        _config["Audiance"],
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials);

            var auth = new AuthModel
            {
                Exp = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Id = user.Id.ToString(),
                Role = roles,

            };
            return auth;

        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<UserForRegisterViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserForRegisterViewModel register)
        {
            CustomerValidator customerValidator = new CustomerValidator();
            ValidationResult resultt = customerValidator.Validate(register);
            if (!resultt.IsValid)
            {
                return BadRequest(resultt.Errors);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        Email = register.Email,
                        UserName = register.Email,
                    };
                    var isPrimaryEmailExist = await _userManager.FindByEmailAsync(user.Email);

                    if (isPrimaryEmailExist != null)
                    {
                        return BadRequest(register.ToResponse());
                    }
                    IdentityResult result = await _userManager.CreateAsync(user, register.Password);

                    if (result.Succeeded)
                    {
                        var appuser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                        await AssignedMemberRoleRole(appuser);
                        await AssignedUserClaim(appuser);

                        return Ok(register.ToResponse());

                    }
                    return BadRequest(register.ToResponse());
                }

            }
            catch (Exception e)
            {
                if (e is ApplicationException)
                {
                    return BadRequest(register.ToResponse());
                }

            }
            return BadRequest(register.ToResponse());
        }


        private async Task AssignedMemberRoleRole(User user)
        {

            var role = await _roleManager.FindByNameAsync(Constants.User_Role);
            if (role == null)
            {
                //asign role
                await _userManager.AddToRoleAsync(user, Constants.User_Role);

            }
            await _userManager.AddToRoleAsync(user, Constants.User_Role);
        }
        private async Task AssignedUserClaim(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
            //if (userClaims ==  )
            //{
            //    //asign claim
            //    await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));

            //}
            await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
        }

        //public static int GenerateVerificationCode()
        //{
        //    return int.Parse(RandomGenerator.Next(0, 999999).ToString("D6"));
        //}
    }
}