using System.Collections.Generic;
using FunWithStore.Domain.Entities;

namespace FunWithStore.WebUI.Models
{
   public class OrdersIndexVM
    {
        public IEnumerable<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public int CustomerId { get; set; }
    }
}
