using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunWithStore.Domain.Entities;

namespace FunWithStore.WebUI.Models
{
  public  class CustomersIndexVM
    {
        public IEnumerable<Customer> Customers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
