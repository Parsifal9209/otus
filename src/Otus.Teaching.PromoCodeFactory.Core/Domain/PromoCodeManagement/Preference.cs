using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    /// <summary>
    /// Предпочтение
    /// </summary>
    public class Preference
        :BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; }

        public ICollection<CustomerPreference> CustomerPreferences { get; set; }
    }
}