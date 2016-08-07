using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FunWithStore.Domain.Entities
{
   public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [DisplayName("Имя покупателя")]
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [DisplayName("Адрес покупателя")]
        [Required(ErrorMessage = "Введите адрес")]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
