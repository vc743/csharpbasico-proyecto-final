using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Data.Dtos;
using ShopApp.Data.Exceptions;
using ShopApp.Data.Interfaces;

namespace ShopApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer customerDb;

        public CustomerController(ICustomer customerDb)
        {
            this.customerDb = customerDb;
        }

        public ActionResult Index()
        {
            var customers = this.customerDb.GetCustomers();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var customer = this.customerDb.GetCustomerById(id);
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAddDto addDto)
        {
            try
            {
                addDto.creation_date = DateTime.Now;
                addDto.creation_user = 2;
                this.customerDb.SaveCustomer(addDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(addDto);
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = this.customerDb.GetCustomerById(id);
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerUpdateDto updateDto)
        {
            try
            {
                updateDto.modify_date = DateTime.Now;
                updateDto.modify_user = 2;
                this.customerDb.UpdateCustomer(updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
