using System;
using System.Collections.Generic;
using System.Linq;
using FunWithStore.Domain.Entities;
using FunWithStore.Domain.Repository.Interfaces;
using FunWithStore.WebUI.Controllers;
using FunWithStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FunWithStore.Tests.ControllerTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        //MethodName_StateUnderTest_ExpectedBehavior

        private readonly Mock<IStoreRepository> mockRepository;

        public CustomerControllerTests()
        {
            mockRepository = new Mock<IStoreRepository>();
            mockRepository.Setup(m => m.GetCustomers()).Returns(new List<Customer>
            {
                new Customer() { CustomerId = 1, Name = "cust1",Address = "city1"},
                new Customer() { CustomerId = 2, Name = "cust2",Address = "city2"},
                new Customer() { CustomerId = 3, Name = "cust3",Address = "city3"},
                new Customer() { CustomerId = 4, Name = "cust4",Address = "city4"},
                new Customer() { CustomerId = 5, Name = "cust5",Address = "city5"}
            });
        }

        [TestMethod]
        public void Index_Paginate_ShouldBeTwoPages()
        {
            //arrange
            CustomerController controller = new CustomerController(mockRepository.Object) {pageSize = 3};

            //act
            CustomersIndexVM result = (CustomersIndexVM)controller.Index(2).Model;

            //assert
            PagingInfo pageInfo = result.PagingInfo;
   
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }


        [TestMethod]
        public void Index_Paginate_ShowTwoItemsOnSecondPage()
        {
            //arrange
            CustomerController controller = new CustomerController(mockRepository.Object) { pageSize = 3 };

            //act
            CustomersIndexVM result = (CustomersIndexVM)controller.Index(2).Model;

            //assert
            Assert.IsTrue(result.Customers.ToList().Count == 2);
        }


        [TestMethod]
        public void Create_AddNewCustomer_CallInsertWithSave()
        {
            //arrange
            CustomerController controller = new CustomerController(mockRepository.Object) { pageSize = 3 };
            var newCustomer = new Customer() {Address = "someAdress",Name = "someName"};

            //act
            controller.Create(newCustomer);

            //assert
            mockRepository.Verify(m => m.InsertCustomer(It.IsAny<Customer>()), Times.Once());
            mockRepository.Verify(m => m.Save(), Times.Once());
        }

        [TestMethod]
        public void Delete_DeleteSomeCustomer_CallDeleteWithSave()
        {
            //arrange
            CustomerController controller = new CustomerController(mockRepository.Object) { pageSize = 3 };

            //act
            controller.Delete(mockRepository.Object.GetCustomers().First().CustomerId);

            //assert
            mockRepository.Verify(m => m.DeleteCustomer(It.IsAny<int>()), Times.Once());
            mockRepository.Verify(m => m.Save(), Times.Once());
        }


        [TestMethod]
        public void Edit_UpdateSomeCustomer_CallUpdateWithSave()
        {
            //arrange
            CustomerController controller = new CustomerController(mockRepository.Object) { pageSize = 3 };
            var firstCust = mockRepository.Object.GetCustomers().First();
            firstCust.Name = "NewName";

            //act
            controller.Edit(firstCust);

            //assert
            mockRepository.Verify(m => m.UpdateCustomer(It.IsAny<Customer>()), Times.Once());
            mockRepository.Verify(m => m.Save(), Times.Once());
        }
    }
}
