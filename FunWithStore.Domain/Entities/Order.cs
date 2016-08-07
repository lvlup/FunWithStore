using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FunWithStore.Domain.Entities
{
  public  class Order
    {
        [Key]
        public int Number { get; set; }

        [DisplayName("Дата заказа")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Стоимость заказа")]
        [Required(ErrorMessage = "Введите стоимость заказа")]
        public int Amount { get; set; }

        [DisplayName("Описание заказа")]
        [Required(ErrorMessage = "Введите описание заказа")]
        public string Description { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
