using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer
        :BaseEntity
    {
        [MaxLength(15)]
        public string FirstName { get; set; }

        [MaxLength(15)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [MaxLength(15)]
        public string Email { get; set; }

        public ICollection<CustomerPreference> CustomerPreferences { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; }
    }
}