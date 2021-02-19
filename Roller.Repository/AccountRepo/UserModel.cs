using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roller.Repository.AccountRepo
{
   public class UserModel
    {
        private string _currentClaim;
        public List<UserModel> Users { get; set; }
        [Required]
        public string Id { get; set; }
        public string Email { get; set; }
        public IList<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
        public string SelectedClaim { get; set; }

        public string CurrentClaim
        {
            get => Claims.Count > 0 ? Claims[0].Type : string.Empty;
            set => _currentClaim = value;
        }
        public class ClaimDto
        {
            public string Type { get; set; }
        }
    }
}
