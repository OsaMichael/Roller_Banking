using Microsoft.AspNetCore.Mvc.ModelBinding;
using Roller.Web.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Roller.Repository.Extensions
{
  public  static class CommonExtension
    {
        public static long ToTimeStamp(this DateTimeOffset date)
        {

            return date.ToUnixTimeMilliseconds();
        }

        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);

            return dtDateTime.DateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<object> ToResponse(this object apiResponse, string message = "")
        {
            return new ApiResponse<object> { Data = apiResponse, Message = message };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ExtractErrorString(this ModelStateDictionary model)
        {
            return string.Join(',', model.
                Values.
                SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)


                );
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value.Trim();
        }

    }
}
