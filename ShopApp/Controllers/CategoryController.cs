using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Data.Dtos;
using ShopApp.Data.Interfaces;

namespace ShopApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory categoryDb;

        public CategoryController(ICategory categoryDb)
        {
            this.categoryDb = categoryDb;
        }
        public ActionResult Index()
        {
            var categories = this.categoryDb.GetCategories();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            id = 3;
            var category = this.categoryDb.GetCategoryById(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryAddDto addDto)
        {
            try
            {
                addDto.creation_date = DateTime.Now;
                addDto.creation_user = 2;
                this.categoryDb.SaveCategory(addDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            id = 3;
            var category = this.categoryDb.GetCategoryById(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryUpdateDto updateDto)
        {
            try
            {
                updateDto.modify_date = DateTime.Now;
                updateDto.modify_user = 2;
                this.categoryDb.UpdateCategory(updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
