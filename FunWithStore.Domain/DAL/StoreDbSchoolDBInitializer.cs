using System;
using System.Collections.Generic;
using System.Data.Entity;
using FunWithStore.Domain.Entities;

namespace FunWithStore.Domain.DAL
{
   public class StoreDBInitializer : CreateDatabaseIfNotExists<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var defaultCustomers = new List<Customer>
            {
                new Customer() {CustomerId = 1, Address = "city1", Name = "CustomerFirst"},
                new Customer() {CustomerId = 2, Address = "city2", Name = "Customer2"},
                new Customer() {CustomerId = 3, Address = "city3", Name = "Customer3"},
                new Customer() {CustomerId = 4, Address = "city4", Name = "Customer4"},
                new Customer() {CustomerId = 5, Address = "city5", Name = "Customer5"},
                new Customer() {CustomerId = 6, Address = "city6", Name = "Customer6"},
                new Customer() {CustomerId = 7, Address = "city7", Name = "Customer7"},
                new Customer() {CustomerId = 8, Address = "city8", Name = "Customer8"}
            };
            defaultCustomers.ForEach(c => context.Customers.Add(c));

            var defaultOrders = new List<Order>
            {
                 new Order()
                {
                    CustomerId = 1,
                    Number = 1,
                    Description = "descr1",
                    Amount = 145,
                    Date = new DateTime(2016, 08, 07)
                },
                new Order()
                {
                    CustomerId = 2,
                    Number = 1,
                    Description = "descr2",
                    Amount = 146,
                    Date = new DateTime(2016, 08, 08)
                },
                new Order()
                {
                    CustomerId = 1,
                    Number = 1,
                    Description = "descr3",
                    Amount = 147,
                    Date = new DateTime(2016, 08, 09)
                },
                new Order()
                {
                    CustomerId = 4,
                    Number = 1,
                    Description = "descr4",
                    Amount = 148,
                    Date = new DateTime(2016, 08, 10)
                },
                new Order()
                {
                    CustomerId = 5,
                    Number = 1,
                    Description = "descr5",
                    Amount = 149,
                    Date = new DateTime(2016, 08, 11)
                },
            };
    
            defaultOrders.ForEach(ord => context.Orders.Add(ord));
            base.Seed(context);
        }
    }
}
