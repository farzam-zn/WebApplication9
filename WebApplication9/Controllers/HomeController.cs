using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Models.Models;
using Utilities;
using WebApplication9.Areas.Admin.ViewModels;
using WebApplication9.ViewModels;

namespace WebApplication5.Controllers
{
	

	
		

		public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IInvoiceRepository _invoiceRepo;
		private readonly ICompanySupplier _companySupplierRepo;
		private readonly IInoviceDetailsRepository _invoicedetailsRepo;
		private readonly IProductReposirory _productRepo;
		private readonly ICategoryRepository _CategoryRepo;
		private readonly IWebHostEnvironment _webHostEnvironment;
		

		public HomeController(ILogger<HomeController> logger,IInvoiceRepository db, IWebHostEnvironment webHostEnvironment, IInoviceDetailsRepository invoicedetailsRepo, ICategoryRepository categoryRepository, IProductReposirory productReposirory, ICompanySupplier companySupplier)
		{
			_invoiceRepo = db;
			_invoicedetailsRepo = invoicedetailsRepo;
			_companySupplierRepo = companySupplier;
			_productRepo = productReposirory;
			_CategoryRepo = categoryRepository;
			_webHostEnvironment = webHostEnvironment;
			_logger = logger;
		}
		public async Task<IActionResult> Index()
        {
			IEnumerable<Product> productList =(List<Product>)await _productRepo.GetAll();
            return View(productList);
        }
		public IActionResult Details(int id)
		{
       Product product = _productRepo.GetById(p=>p.Id==id);
            return View(product);





            /*			var product = _productRepo.GetById(x => x.Id == id);
                        productViewModel productViewModel = new productViewModel { ProductId = product.Id,
                        ProductName = product.Name, 
                        ProductDescription = product.Description,
                        PriceFor100=product.Price100,
                        PriceFor50=product.Price50,
                        ImageUrl= product.ImageUrl,
                        count=1,
                        ProductImages =1
                        };*/
            /*			var productViewModel = _productRepo.Getp(id).Select(p => new productViewModel
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            ProductDescription = product.Description,
                            PriceFor100 = product.Price100,
                            PriceFor50 = product.Price50,
                            ListPrice = product.ListPrice,
                            ProductCategoryId = product.CategoryId,
                            ImageUrl = product.ImageUrl,
                        });*/


            /*  var productsRemaining = _productRepo.GetIt().Select(p => new RemainingViewModel
              {
                  Id = p.Id,
                  CategoryId = p.CategoryId,
                  Name = p.Name,
                  Color = p.Color,
                  Category = new Category { Name = p.Category.Name },
                  SellQuantity = yyy.Where(io => io.Id == p.Id).SelectMany(i => i.InvoiceDetails).Sum(t => t.Quantity) - p.Quantity,
                  Quantity = yyy.Where(j => j.Id == p.Id).SelectMany(op => op.InvoiceDetails).Sum(tt => tt.Quantity),
                  RemainingQuantity = p.Quantity,
                  ListPrice = p.ListPrice,
                  Price100 = p.Price100,
                  Price50 = p.Price50,

              }).ToList();
  */


		}
        public IActionResult Privacy()
        {

            return View("Errorr");
        }


    }
}