//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using Roller.DataContext.Entity;
//using Roller.Repository.Extensions;
//using Roller.Web.Models;

//namespace Roller.Web.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class AccountsController : ControllerBase
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly RoleManager<Role> _roleManager;
//        private readonly IPasswordHasher<User> _passwordHasher;
//        private readonly IConfiguration _config;
//        private readonly ILogger _logger;

//        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, 
//            RoleManager<Role> roleManager, IPasswordHasher<User> passwordHasher, IConfiguration config,
//            ILogger<AccountsController> logger)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _passwordHasher = passwordHasher;
//            _config = config;
//            _logger = logger;
//        }

//        [HttpPost("register")]
//        [ProducesResponseType(typeof(ApiResponse<UserForRegisterViewModel>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [AllowAnonymous]
//        public async Task<IActionResult> Register([FromBody]UserForRegisterViewModel register)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var user = new User
//                    {
//                        Email = register.Email,
//                        UserName = register.Email,
//                    };
//                    var isPrimaryEmailExist = await _userManager.FindByEmailAsync(user.Email);

//                    if (isPrimaryEmailExist != null)
//                    {
//                        return BadRequest(register.ToResponse());
//                    }
//                    IdentityResult result = await _userManager.CreateAsync(user, register.Password);

//                    if (result.Succeeded)
//                    {
//                        var appuser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
//                        await AssignedMemberRoleRole(appuser);
//                        await AssignedUserClaim(appuser);

//                        return Ok(register.ToResponse());

//                    }
//                    return BadRequest(register.ToResponse());
//                }

//            }
//            catch (Exception e)
//            {
//                if (e is ApplicationException)
//                {
//                    return BadRequest(register.ToResponse());
//                }

//            }
//            return BadRequest(register.ToResponse());
//        }

//        private async Task AssignedMemberRoleRole(User user)
//        {

//            var role = await _roleManager.FindByNameAsync(Constants.User_Role);
//            if (role == null)
//            {
//                //asign role
//                await _userManager.AddToRoleAsync(user, Constants.User_Role);

//            }
//            await _userManager.AddToRoleAsync(user, Constants.User_Role);
//        }
//        private async Task AssignedUserClaim(User user)
//        {
//            var userClaims = await _userManager.GetClaimsAsync(user);
//            await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
//            //if (userClaims ==  )
//            //{
//            //    //asign claim
//            //    await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
              
//            //}
//            await _userManager.AddClaimAsync(user, new Claim(Claims.User, "true"));
//        }


//        [HttpPost("login")]
//        [ProducesResponseType(typeof(ApiResponse<UserForLoginViewModel>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login([FromBody] UserForLoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // This doesn't count login failures towards account lockout
//                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
//                var user = await _userManager.Users.SingleOrDefaultAsync(r => r.Email == model.Email);
//                if (user == null)
//                {
//                    user = await _userManager.Users.SingleOrDefaultAsync(r => r.UserName == model.Email);
//                    if (user == null)
//                    {
//                        return BadRequest(model.ToResponse());
//                    }
//                }
//                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
//                {
//                    await _signInManager.SignInAsync(user, model.RememberMe);
//                    var token = await GenerateJwtToken(user.Email, user);

//                    return Ok(token);
//                }

//            }
//            // If we got this far, something failed, redisplay form
//            return BadRequest("Validation Failed: {ModelState.ExtractErrorString()}");
//        }
//        private async Task<AuthModel> GenerateJwtToken(string email, User user)
//        {
//            var userClaims = await _userManager.GetClaimsAsync(user);
//            IList<string> role = await _userManager.GetRolesAsync(user);
//            string roles = string.Empty;
//            if (role.Any())
//            {
//                roles = role.Join();
//            }

//            var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, email),
//                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
//                new Claim(ClaimTypes.NameIdentifier, user.UserName),
//                new Claim("email", user.Email),
//                new Claim("username", user.UserName),
//                new Claim("roles",roles),
//              //  new Claim("companyName",user.CompanyName),


//            }.Union(userClaims);

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:JwtKey"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(_config["AppSettings:Expiration"]));
//            var token = new JwtSecurityToken(
//                _config["AppSettings:JwtIssuer"],
//                _config["AppSettings:JwtIssuer"],
//                claims,
//                expires: expires,
//                signingCredentials: creds
//            );

//            var auth = new AuthModel
//            {
//                Exp = token.ValidTo,
//                Token = new JwtSecurityTokenHandler().WriteToken(token),
//                Id = user.Id.ToString(),
//                Role = roles,

//            };
//            return auth;

//        }
//        private bool VerifyPasswordHash(string password, string passwordHash)
//        {
//            using (var hmac = new System.Security.Cryptography.HMACSHA512())
//            {
//                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//                for (int i = 0; i < computedHash.Length; i++)
//                {
//                    if (computedHash[i] != passwordHash[i]) return false;
//                }
//            }
//            return true;
//        }

//        //// GET: api/Account
//        //[HttpGet]
//        //public IEnumerable<string> Get()
//        //{
//        //    return new string[] { "value1", "value2" };
//        //}

//        //// GET: api/Account/5
//        //[HttpGet("{id}", Name = "Get")]
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}

//        //// POST: api/Account
//        //[HttpPost]
//        //public void Post([FromBody] string value)
//        //{
//        //}

//        //// PUT: api/Account/5
//        //[HttpPut("{id}")]
//        //public void Put(int id, [FromBody] string value)
//        //{
//        //}

//        //// DELETE: api/ApiWithActions/5
//        //[HttpDelete("{id}")]
//        //public void Delete(int id)
//        //{
//        //}
//    }
//}
