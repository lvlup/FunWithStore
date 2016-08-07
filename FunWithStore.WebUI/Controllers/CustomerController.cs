using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FunWithStore.Domain.Entities;
using FunWithStore.Domain.Repository.Interfaces;
using FunWithStore.WebUI.Models;

namespace FunWithStore.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IStoreRepository storeRepository;
        public int pageSize = 4;

        public CustomerController(IStoreRepository repository)
        {
            storeRepository = repository;
        }


        // GET: Customer
        public ViewResult Index(int page = 1)
        {
            CustomersIndexVM model = new CustomersIndexVM()
            {
                Customers = storeRepository.GetCustomers().
                    OrderBy(c => c.CustomerId).
                    Skip((page - 1)*pageSize).
                    Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = storeRepository.GetCustomers().Count()
                }
            };
            
            return View(model);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storeRepository.InsertCustomer(customer);
                    storeRepository.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
            }
            return View();
        }

        public ActionResult Edit(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = storeRepository.GetCustomers().FirstOrDefault(c => c.CustomerId == customerId);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storeRepository.UpdateCustomer(customer);
                    storeRepository.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
            }
            return View();
        }


        public ActionResult Delete(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            storeRepository.DeleteCustomer((int) customerId);
            storeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}