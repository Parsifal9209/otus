using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class PromoCode
        : BaseEntity
    {
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(30)]
        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(30)]
        public string PartnerName { get; set; }

        /// <summary>
        /// Сотрудник который создал промокод
        /// </summary>
        public Employee PartnerManager { get; set; }

        public Guid PartnerManagerId { get; set; }

        public Preference Preference { get; set; }

        public Guid PreferenceId { get; set; }

        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }
    }
}