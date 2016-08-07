using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FunWithStore.Domain.Entities;
using FunWithStore.Domain.Repository.Interfaces;
using FunWithStore.WebUI.Controllers;
using FunWithStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FunWithStore.Tests.ControllerTests
{
    [TestClass]
    public class OrderControllerTests
    {

        private readonly Mock<IStoreRepository> mockRepository;

        public OrderControllerTests()
        {
            mockRepository = new Mock<IStoreRepository>();
            mockRepository.Setup(m => m.GetOrders()).Returns(new List<Order>
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
                }
            });
        }


        [TestMethod]
        public void Index_OrdersForCustomerWithIdOne_ShowTwoOrders()
        {
            //arrange
            OrderController controller = new OrderController(mockRepository.Object) {pageSize = 3};

            //act
            ViewResult viewResult = (ViewResult) controller.Index(1);
            OrdersIndexVM result = (OrdersIndexVM)viewResult.Model;

            //assert
            var orders = result.Orders.ToList();

            Assert.AreEqual(orders.Count, 2);
        }


        [TestMethod]
        public void Create_AddNewOrder_CallInsertWithSave()
        {
            //arrange
            OrderController controller = new OrderController(mockRepository.Object) { pageSize = 3 };
            var newOrder = new Order()
            {
                CustomerId = 6,
                Number = 6,
                Description = "descr6",
                Amount = 1478,
                Date = new DateTime(2016, 08, 10)
            };

            //act
            controller.Create(newOrder);

            //assert
            mockRepository.Verify(m => m.InsertOrder(It.IsAny<Order>()), Times.Once());
            mockRepository.Verify(m => m.Save(), Times.Once());
        }

        [TestMethod]
        public void Delete_DeleteOrder_CallDeleteWithSave()
        {
            //arrange
            OrderController controller = new OrderController(mockRepository.Object) { pageSize = 3 };
            var firstOrder = mockRepository.Object.GetOrders().First().Number;

            //act
            controller.Delete(firstOrder);

            //assert
            mockRepository.Verify(m => m.DeleteOrder(It.IsAny<int>()), Times.Once());
            mockRepository.Verify(m => m.Save(), Times.Once());
        }
    }
}
