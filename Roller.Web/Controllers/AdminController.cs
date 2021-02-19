using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Roller.DataContext.Entity;
using Roller.DContext;
using Roller.Repository.AccountRepo;
using Roller.Repository.UserClaim;
using Roller.Web.Models;
using Roller.Web.Models.RoleViewModel;
using Roller.Web.Models.UserViewModels;
using static Roller.Web.Controllers.Common.Enum;

namespace Roller.Web.Controllers
{
    [Authorize(Policy = Claims.Admin)]
    public class AdminController : BaseController
    {
        private UserManager<User> _userManager;
        private readonly IAccountManager _accountManager;
        private readonly IMapper _mapper;
        private IPasswordHasher<User> _passwordHasher;
        private IPasswordValidator<User> _passwordValidator;
        private IUserValidator<User> _userValidator;
        private RoleManager<Role> roleManager;
        private RollerDataContext _context;
        private IClaimRepo _claim;
        public AdminController(RoleManager<Role> roleMgr, UserManager<User> userManager, IAccountManager accountManager, IMapper mapper,
           IPasswordHasher<User> passwordHasher, IPasswordValidator<User> passwordValidator, 
           IUserValidator<User> _userValidator, IClaimRepo claim, RollerDataContext context)
        {
            roleManager = roleMgr;
            _userManager = userManager;
            _accountManager = accountManager;
            _mapper = mapper;
            _claim = claim;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // var results = _userManager.Users;
            var results = await _accountManager.GetAllUsersWithClaims();
            UserViewModel mappedUser = _mapper.Map<UserViewModel>(results);

            var me = new UserModel
            {
                 Email = mappedUser.Email,
                 Users = mappedUser.Users,
                  Id = mappedUser.Id
            };

          // var myMapp = mappedUser.Users;
            //var userClaims = await _userManager.GetClaimsAsync();
            return View(me);
        }
        public async Task<IActionResult> UserList()
        {
           // var results = _userManager.Users;
            var results = await _accountManager.GetUsers();

            List<UserViewWebModel> mappedUser = _mapper.Map<List<UserViewWebModel>>(results);
            return View(mappedUser);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = Claims.Admin)]
        public async Task<IActionResult> CreateUser(Models.UserViewModels.UserViewWebModel user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User
                {
                    UserName = user.FirstName + user.LastName,
                    Email = user.Email,
                    Age = user.Age,
                    Salary = user.Salary
                };

                var mappedUser = _mapper.Map<User>(appUser);

               // await UserManager.AddClaimAsync(appUser, new Claim("sub", user.Id.ToString()));
                var resultId = await _accountManager.CreateUserAsync(mappedUser, user.Password);

                //var g = _userManager.AddClaimAsync(mappedUser, new Claim("User", mappedUser.Id.ToString()));
                //resultId = Convert.ToBoolean(g);
                if (resultId)
                {
                    //await _userManager.AddToRoleAsync(user, registerViewModel.UserRoles);
                    return RedirectToAction("UserList");
                }
            }
            return View(user);
        }
        //public async Task<IActionResult> EditClaims(string id)
        //{
        //     var results = _userManager.Users;
        //    //var resultr = await _userManager.GetClaimsAsync();
        //   // var results = await _claim.GetClaims();

        //    List<UserViewModel> mappedUser = _mapper.Map<List<UserViewModel>>(results);
        //    return View(mappedUser);
        //}
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModel model)
        {
            User user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            { 
                user.Email = model.NewEmail;
                user.PasswordHash = model.Password;
               // _context.Users.Add(user);
                _context.SaveChanges();
            return View(user);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Policy = Claims.Admin)]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
              //  var users = await _accountManager.GetUsers();
               //var resultt = _claim.GetClaims(model);

                ChangeClaim claim = new ChangeClaim
                {
                     NewClaim = model.SelectedClaim,
                     UserId = model.Id,
                     CurrentClaim = model.CurrentClaim
                };
                var result = await _claim.GetClaims(claim);

                if (result != null)
                {
                    //alert message
                    TempData["Message"] = "New user role " + " " + result.NewClaim + " " + "Successful";

                    dynamic transRef = TempData["Message"];

                    Alert("success", transRef, NotificationType.success);
                }
                else
                {
                    //TempData["ClaimMessage"] = result.Error;
                }
            }
            //return RedirectToAction();
           return RedirectToAction(nameof(Index));
        }
        /// <summary>
        // role index
        /// </summary>
        /// <returns></returns>
        public ViewResult RoleList()
        {
            var result = roleManager.Roles;
            List<RoleModel> RoleList = _mapper.Map<List<RoleModel>>(result);
            return View(RoleList);
        }
        // create role
        public IActionResult CreateRole() => View();
        [HttpPost]
        [Authorize(Policy = Claims.Admin)]
        public async Task<IActionResult> CreateRole([Required]string name)
        {
            if (ModelState.IsValid)
            {
            //  IdentityResult result = await roleManager.CreateAsync(name);
                var role = new Role
                {
                    Name = name
                };
                // var mappedUser = _mapper.Map<Role>(name);
                IdentityResult result = await roleManager.CreateAsync(role);

              //  RoleModel roles = _mapper.Map<RoleModel>(result);

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
               
                else
                {
                    Errors(result);
                }
                // TempData["Message"] = result;
                //return View(roles);
            }
            return View(name);
        }
        // user parts
        public async Task<IActionResult> Users()
        {
            var users = await _accountManager.GetUsers();

            List<Models.UserViewModels.UserViewWebModel> userList = _mapper.Map<List<Models.UserViewModels.UserViewWebModel>>(users);
            return View(userList);
        }
       

        public async Task<IActionResult> UpdateUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, string email, string password, int age, string country, string salary)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult validEmail = null;
                if (!string.IsNullOrEmpty(email))
                {
                    validEmail = await _userValidator.ValidateAsync(_userManager, user);
                    if (validEmail.Succeeded)
                        user.Email = email;
                    else
                        Errors(validEmail);
                }
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    else
                        Errors(validPass);
                }
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                user.Age = age;

                //  Country myCountry;
                //  Enum.TryParse(country, out myCountry);
                //  user.Country = myCountry;

                if (!string.IsNullOrEmpty(salary))
                    user.Salary = salary;
                else
                    ModelState.AddModelError("", "Salary cannot be empty");

                if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded && !string.IsNullOrEmpty(salary))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public async Task<IActionResult> Roles()
        {

            var roles = await _accountManager.GetRoles();

            List<RoleModel> roleList = _mapper.Map<List<RoleModel>>(roles);
            return View(roleList);
        }

        //public async Task<IActionResult> UpdateClaim(string usid)
        //{
        //    //  IdentityRole role = await roleManager.FindByIdAsync(id);

        //    var use = await _accountManager.GetUsers();
        //    var clam = await  _userManager.GetUserIdAsync();
        //    List<User> members = new List<User>();
        //    List<User> nonMembers = new List<User>();

        //    var listOfUsers = _userManager.Users;
        //    foreach (User user in listOfUsers)
        //    {
        //        var list = await _userManager.IsInRoleAsync(user, rolee.Name) ? members : nonMembers;
        //        list.Add(user);
        //    }
        //    // RoleEditModel smodel = _mapper.Map<RoleEditModel>(rolee);
        //    var me = new RoleEditModel
        //    {
        //        Role = rolee,
        //        Members = members,
        //        NonMembers = nonMembers
        //    };

        //    return View(me);
        //}

        public async Task<IActionResult> Update(int id)
        {
          //  IdentityRole role = await roleManager.FindByIdAsync(id);
            var rolee = await _accountManager.GetRoleByIdAsync(id);

            //User user = new User();
            //var clam = await _accountManager.GetUsersClaims(user);

            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();

            var listOfUsers = _userManager.Users;
            foreach (User user in listOfUsers)
            {
                var list = await _userManager.IsInRoleAsync(user, rolee.Name) ? members : nonMembers;
                list.Add(user);
            }
           // RoleEditModel smodel = _mapper.Map<RoleEditModel>(rolee);
            var me = new RoleEditModel
            {
                Role = rolee,
                Members = members,
                NonMembers = nonMembers
            };

            return View(me);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            TempData[""] = "";
                        //  Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            TempData[""] = "";
                        // Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Roles));
            else
                return await Update(model.RoleId);
        }
        //void Errors(IdentityResult result)
        //{
        //    foreach (IdentityError error in result.Errors)
        //        ModelState.AddModelError("", error.Description);
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users);
        }
    }
}