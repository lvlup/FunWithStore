using System.Collections.Generic;
using FunWithStore.Domain.Entities;

namespace FunWithStore.Domain.Repository.Interfaces
{
    public interface IStoreRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Customer> GetCustomers();

        void InsertCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int customerId);

        void InsertOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(int orderId);

        void Save();
    }
}
