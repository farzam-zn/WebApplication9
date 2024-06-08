using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Models.Models;
using DataAccess.Repository.IRepository;
using DataAccess;

namespace WebApplication9.Areas.Admin.Controllers
{
   // [Area="Admin"]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public async Task<IActionResult> Category(string error)
        {
            List<Category> objcategoryList = (List<Category>)await _categoryRepo.GetAll();

            ViewBag.Error = error;
            return View(objcategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category x)
        {
            _categoryRepo.Add(x);
            _categoryRepo.Save();
            return RedirectToAction("Category");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            // Category categoryFromDb = cantext.categories.Find(id);
            Category category = _categoryRepo.GetById(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            //	Category category1 = context.categories.FirstOrDefault(c => c.Id == id);
            //	Category category2 = context.categories.Where(c => c.Id == id).FirstOrDefault();


            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            return RedirectToAction("Category");
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            // Category categoryFromDb = cantext.categories.Find(id);
            Category category = _categoryRepo.GetById(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            //Category category1 = context.categories.FirstOrDefault(c => c.Id == id);
            //Category category2 = context.categories.Where(c => c.Id == id).FirstOrDefault();


            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Category obj)
        {
            try
            {
                _categoryRepo.Delete(obj);
                _categoryRepo.Save();

                return RedirectToAction("Category");

            }
            catch
            {

                return RedirectToAction("Category", new { error = "error" });
            }
        }
        public JsonResult AjaxMethod(string name)

        {

            Category category1 = new Category
            {
                Id = 4,
                Name = name

            };


            return Json(category1);
        }
    }
}
