using System.Collections.Generic;
using System.Data.Entity;
using FunWithStore.Domain.DAL;
using FunWithStore.Domain.Entities;
using FunWithStore.Domain.Repository.Interfaces;

namespace FunWithStore.Domain.Repository.Implementations
{
    public class StoreRepository : IStoreRepository
    {
        readonly StoreContext context = new StoreContext();

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers;
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = context.Customers.Find(customerId);
            context.Customers.Remove(customer);
        }

        public void InsertOrder(Order order)
        {
            context.Orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
        }

        public void DeleteOrder(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            context.Orders.Remove(order);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
