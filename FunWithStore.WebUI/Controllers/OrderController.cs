using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FunWithStore.Domain.Entities;
using FunWithStore.Domain.Repository.Interfaces;
using FunWithStore.WebUI.Models;

namespace FunWithStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IStoreRepository storeRepository;

        public int pageSize = 10;

        public OrderController(IStoreRepository repository)
        {
            storeRepository = repository;
        }

        // GET: Order
        public ActionResult Index(int customerId, int page = 1)
        {
            OrdersIndexVM model = new OrdersIndexVM()
            {
                Orders = storeRepository.GetOrders().
                    Where(ord => ord.CustomerId == customerId).
                    OrderBy(c => c.CustomerId).
                    Skip((page - 1) * pageSize).
                    Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = storeRepository.GetCustomers().Count()
                },
                CustomerId = customerId
            };

            return View(model);
        }


        public ViewResult Create(int customerId)
        {
            return View(new Order() {CustomerId = customerId});
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.Date = DateTime.Now;

                    storeRepository.InsertOrder(order);
                    storeRepository.Save();

                    return RedirectToAction("Index", new { customerId = order.CustomerId });
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
                }
            }
            return View();
        }

        public ActionResult Edit(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = storeRepository.GetOrders().FirstOrDefault(c => c.Number == orderId);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storeRepository.UpdateOrder(order);
                    storeRepository.Save();

                    return RedirectToAction("Index", new { customerId = order.CustomerId });
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
            }
           
            return View();
        }


        public ActionResult Delete(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = storeRepository.GetOrders().FirstOrDefault(c => c.Number == orderId);

            storeRepository.DeleteOrder((int) orderId);
            storeRepository.Save();

            return RedirectToAction("Index", new { customerId = order.CustomerId });
        }
    }
}