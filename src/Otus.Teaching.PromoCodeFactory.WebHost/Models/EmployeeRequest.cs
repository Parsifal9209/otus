using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public List<RoleItemRequest> Roles { get; set; }

        [Required]
        public int AppliedPromocodesCount { get; set; }
    }
}
