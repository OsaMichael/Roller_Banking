using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roller.Repository.UserClaim
{
   public interface IClaimRepo
    {
        Task<ChangeClaim> GetClaims(ChangeClaim request);
    }
}
