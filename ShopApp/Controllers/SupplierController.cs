using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Data.Dtos;
using ShopApp.Data.Interfaces;

namespace ShopApp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplier supplierDb;

        public SupplierController(ISupplier supplierDb)
        {
            this.supplierDb = supplierDb;
        }

        public ActionResult Index()
        {
            var suppliers = this.supplierDb.GetSuppliers();
            return View(suppliers);
        }

        // GET: SupplierController/Details/5
        public ActionResult Details(int id)
        {
            var supplier = this.supplierDb.GetSupplierById(id);
            return View(supplier);
        }

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierAddDto addDto)
        {
            try
            {
                addDto.creation_date = DateTime.Now;
                addDto.creation_user = 2;
                this.supplierDb.SaveSupplier(addDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(addDto);
            }
        }

        // GET: SupplierController/Edit/5
        public ActionResult Edit(int id)
        {
            var supplier = this.supplierDb.GetSupplierById(id);
            return View(supplier);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierUpdateDto updateDto)
        {
            try
            {
                updateDto.modify_date = DateTime.Now;
                updateDto.modify_user = 2;
                this.supplierDb.UpdateSupplier(updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
