using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Data.Dtos;
using ShopApp.Data.Interfaces;

namespace ShopApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct productDb;

        public ProductController(IProduct productDb)
        {
            this.productDb = productDb;
        }

        public ActionResult Index()
        {
            var products = this.productDb.GetProducts();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = this.productDb.GetProductById(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductAddDto addDto)
        {
            try
            {
                addDto.creation_date = DateTime.Now;
                addDto.creation_user = 2;
                this.productDb.SaveProduct(addDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(addDto);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = this.productDb.GetProductById(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductUpdateDto updateDto)
        {
            try
            {
                updateDto.modify_date = DateTime.Now;
                updateDto.modify_user = 2;
                this.productDb.UpdateProduct(updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
