//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Roller.Repository.Customers;
//using Roller.Repository.Extensions;

//namespace Roller.Web.Areas.API.Controllers
//{
//    [ApiController]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    public class CustomersController : ControllerBase
//    {
//        // GET: /api/me
//        [HttpGet("/api/me")]
//        public IActionResult MyProfile()
//        {
//            if (User.HasClaim(c => c.Type == ClaimTypes.Name))
//            {
//                var email = User.Identity.Name;
//                try
//                {
//                    var userDto =  new GetCustomerDetailsQuery { CustomerEmail = email };
//                    return Ok(userDto);
//                }
//                catch (NotFoundException)
//                {
//                    return NotFound("Customer not found");
//                }
//            }
//            return BadRequest("Error occured while getting your data, email not found. Have you typed it correctly?");
//        }

//        //public IActionResult Index()
//        //{
//        //    return View();
//        //}
//    }
//}