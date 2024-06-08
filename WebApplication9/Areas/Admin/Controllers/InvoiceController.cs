using System.Collections.Generic;
using System.Diagnostics;
using DataAccess.Migrations;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using WebApplication9.ViewModels;
namespace WebApplication9.Controllers;
public class InvoiceController : Controller
{

    private readonly IInvoiceRepository _invoiceRepo;
    private readonly ICompanySupplier _companySupplierRepo;
    private readonly IInoviceDetailsRepository _invoicedetailsRepo;
    private readonly IProductReposirory _productRepo;
    private readonly ICategoryRepository _CategoryRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public InvoiceController(IInvoiceRepository db, IWebHostEnvironment webHostEnvironment, IInoviceDetailsRepository invoicedetailsRepo, ICategoryRepository categoryRepository, IProductReposirory productReposirory, ICompanySupplier companySupplier)
    {
        _invoiceRepo = db;
        _invoicedetailsRepo = invoicedetailsRepo;
        _companySupplierRepo = companySupplier;
        _productRepo = productReposirory;
        _CategoryRepo = categoryRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Invoice()
    {

        //List<InvoiceDetails> invoiceDetails1= new List<InvoiceDetails>();

        List<Invoice> invoices = _invoiceRepo.Convert().ToList();

        List<Inv> inv = new List<Inv>();

        foreach (var t in invoices)
        {
            inv.Add(new Inv
            {
                Id = t.Id,
                InvoiceNumber = t.InvoiceNumber,
                NumberofItems = t.NumberofItems,
                Sname = t.CompanySupplier.SupplierName,
                CreationDate = t.CreationDate,
            });
        }



     /*   foreach (var a in invoices)
        {
            a.SName = _companySupplierRepo.Get(i => i.CSId == a.SupplierId).ToString();

        }*/
        return View(inv);
    }
    public async Task<IActionResult> InvoiceCreate()
    {
        var SupplierData = (List<CompanySupplier>)await _companySupplierRepo.GetAll();
        ProductView productView = new ProductView();
        productView.SupplierNameList = new List<SelectListItem>();

        foreach (var a in SupplierData)
        {
            productView.SupplierNameList.Add(new SelectListItem
            {
                Text = a.SupplierName,
                Value = Convert.ToString(a.CSId)
            });
        }
        return View(productView);

    }
    [HttpPost]
    public async Task<IActionResult> InvoiceCreate(ProductView x)
    {
        Invoice invoice = x.invoice;
        invoice.CreationDate = DateTime.Now;
        _invoiceRepo.Add(invoice);
        _invoiceRepo.Save();
        return RedirectToAction("Test", invoice);
    }

    [HttpPost]
    public IActionResult InvoiceDelete(Invoice obj)
    {
        var id = obj.Id;
        var invoiceDetails = _invoicedetailsRepo.GetById(x => x.InvoiceId == id);
        _invoiceRepo.Delete(obj);
        _invoicedetailsRepo.Delete(invoiceDetails);
        _invoicedetailsRepo.Save();
        _invoiceRepo.Save();
        return RedirectToAction("Invoice");
    }

    public IActionResult ViewDetails(int id)
    {
        //Invoice invoice2 = context.Invoices.FirstOrDefault(c => c.Id == s.Id);
        //var invoicedetails = _invoicedetailsRepo.find(s);



        InvoiceDetailsList invoiceDetailsList = new InvoiceDetailsList
        {
            InvoiceDetails = _invoicedetailsRepo.include(id).ToList(),
            InvoiceId = id,
        };



        return View(invoiceDetailsList);
    }
    public async Task<IActionResult> AddDetails(int InvoiceId)
    {
        var theinvoice = _invoiceRepo.GetById(p => p.Id == InvoiceId);
        //Where(x => x.Id == invoiceId
        ProductView productView = new ProductView();
        productView.Listofproducts = new List<SelectListItem>();
        productView.Companylist = new List<SelectListItem>();
        productView.LastInvoiceToChangeid = InvoiceId;
        //productView.LastInvoiceToChange = TheInvoice;
        productView.Listofproducts.Select(n => new SelectListItem
        {
            Value = n.Value,
            Text = n.Text
        }).ToList();
        var ProductsData = (List<Product>)await _productRepo.GetAll();
        foreach (var n in ProductsData)
        {
            productView.Listofproducts.Add(new SelectListItem
            {
                Text = n.Name,
                Value = Convert.ToString(n.Id)
            });
        }





        return View(productView);
    }
    [HttpPost]
    public IActionResult AddDetails(ProductView R, int idd)
    {
        var c = _invoiceRepo.GetById(a => a.Id == idd);
        //var x = context.Invoices.Where(a=>a.Id==R.LastInvoiceToChangeid);
        //var x = context.Invoices.FirstOrDefault(a => a.Id == idd);
        InvoiceDetails invoiceDetailss = R.invoiceDetails;
        invoiceDetailss.InvoiceId = idd;
        _invoicedetailsRepo.Add(invoiceDetailss);
        _invoicedetailsRepo.Save();
        //R.invoiceDetails.InvoiceId = R.invoice.Id;
        //R.invoiceDetails.ProductId = R.Product.Id;
      
        return RedirectToAction("ViewDetails", c);
    }
   /* public async Task<IActionResult> CreateProduct()
    {
        var CategoryData = (List<Category>)await _CategoryRepo.GetAll();
        CategoryProdcut categoryproduct = new CategoryProdcut();
        categoryproduct.ListOfCategories = new List<SelectListItem>();
        foreach (var x in CategoryData)
        {
            categoryproduct.ListOfCategories.Add(new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.Id)
            });
        }



        return View(categoryproduct);
    }


    [HttpPost]
    public IActionResult CreateProduct(Product product)
    {
        return RedirectToAction("ProductList", new { success = "success" });
    }*/

    public IActionResult InvoiceDetailsDelete(int x)
    {
        if (x == 0 || x == null)
        {
            return NotFound();
        }

        InvoiceDetails invoiceDetails = _invoicedetailsRepo.GetById(s => s.Id == x);
        // Category categoryFromDb = cantext.categories.Find(id);
        //Invoice invoice2 = context.Invoices.Find(id);
        if (invoiceDetails == null)
        {
            return NotFound();
        }
        //InvoiceDetails invoiceDetails = context.InvoicesDetails.FirstOrDefault(s => s.Id == x);
        //InvoiceDetails invoicedet2 = context.InvoicesDetails.FirstOrDefault(c => c.Id == x);
        // Category categoryFromDb = cantext.categories.Find(id);
        //Invoice invoice2 = context.Invoices.Find(id);
        return View(invoiceDetails);
    }

    [HttpPost]
    public IActionResult DeleteInvoiceDetails(int detaiId)
    {
        var d = _invoicedetailsRepo.GetById(p => p.Id == detaiId);

        //InvoiceDetails invoiceDetails = context.InvoicesDetails.FirstOrDefault(x => x.InvoiceId == d.Id);
        _invoicedetailsRepo.Delete(d);
        _invoicedetailsRepo.Save();
        var invoice = _invoiceRepo.GetById(p => p.Id == d.InvoiceId);

        return RedirectToAction("ViewDetails", invoice);

    }




    public async Task<IActionResult> InvoiceEdit(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        ProductView productview = new ProductView();
        productview.Listofproducts = new List<SelectListItem>();

        var ProductsData = (List<Product>)await _productRepo.GetAll();
        foreach (var n in ProductsData)
        {
            productview.Listofproducts.Add(new SelectListItem
            {
                Text = n.Name,
                Value = Convert.ToString(n.Id)
            });
        }

        // Category categoryFromDb = cantext.categories.Find(id);
        //InvoiceDetailsController invoiceDetails = context.InvoicesDetails.Find(id);
        InvoiceDetails invoiceDetails = _invoicedetailsRepo.GetById(u => u.Id == id);
        productview.LastInvoiceToChangeid = id;
        productview.invoiceDetails = invoiceDetails;
        if (invoiceDetails == null)
        {
            return NotFound();
        }
        //Category category1 = context.categories.FirstOrDefault(c => c.Id == id);
        //Category category2 = context.categories.Where(c => c.Id == id).FirstOrDefault();


        return View(productview);
    }
    [HttpPost]
    public IActionResult InvoiceEdit(ProductView obj, int id)
    {

        //InvoiceDetails invoiceDetails = obj.invoiceDetails;
        //.InvoiceId =x.Id ;
        //var invoice = context.Invoices.SingleOrDefault(p => p.Id == obj.invoiceDetails.InvoiceId);
        var invoice = _invoiceRepo.GetById(u => u.Id == obj.invoiceDetails.InvoiceId);

        //var x = context.Invoices.FirstOrDefault(a => a.Id == id);
        InvoiceDetails invoiceDetails = obj.invoiceDetails;
        invoiceDetails.Id = id;
        //obj.invoiceDetails.Id= ;
        //var invoice = context.Invoices.FirstOrDefault(p => p.Id == x.InvoiceId);
        _invoicedetailsRepo.Update(invoiceDetails);
        _invoicedetailsRepo.Save();
        _invoiceRepo.Save();
        return RedirectToAction("ViewDetails", invoice);
    }


    public async Task<IActionResult> ProductList(string error, string success)
    {
        var x = (List<Product>)await _productRepo.GetAll();
        var yyy = _productRepo.GetProductsInclude().ToList();
        var xx = x.Select(aa => aa.Quantity).ToList();



        var products = _productRepo.GetIt().ToList();
        products = products.Select(p => { p.Quantity = p.InvoiceDetails.Sum(s => s.Quantity); return p; }).ToList();
        var productsRemaining = _productRepo.GetIt().Select(p => new RemainingViewModel
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

        //var productsRemainingg = yyy.SelectMany(i => i.InvoiceDetails).Sum(t => t.Quantity) - yyy.Quantity });
        var Ind = (List<InvoiceDetails>)await _invoicedetailsRepo.GetAll();
        var remainingQuantity = x.Select(s => s.Quantity).ToList();
        RemainingViewModel remainingViewModel = new RemainingViewModel();
        List<int> ints = new List<int>();


        SaleViewModel saleviewmodel = new SaleViewModel();

        RemainingViewModel remainingViewModel1 = new RemainingViewModel();
        foreach (var i in productsRemaining)
        {

            remainingViewModel1.Id = i.Id;
            remainingViewModel1.Name = i.Name;
            remainingViewModel1.CategoryId = i.CategoryId;
            remainingViewModel1.sellQu = (i.Quantity);
        }

        var category = (List<Category>)await _CategoryRepo.GetAll();

        var query = _productRepo
              .GetIt().GroupBy(a => new { a.Id, a.Name, CategoryName = a.Category.Name, a.Color }).Select(pp => new RemainingViewModel
              {
                  Id = pp.Key.Id,
                  Name = pp.Key.Name,

                  Color = pp.Key.Color,
                  Quantity = pp.SelectMany(p => p.InvoiceDetails).Sum(x => x.Quantity)
              });
                  
               /*   SellQuantity = productsRemaining.Select(oo => oo.Quantity)
              }).ToList()*/;


        ViewBag.Seccess = success;
        ViewBag.Error = error;


        return View(productsRemaining);


    }


    public IActionResult ProductDelete(int id)
    {

        try
        {

            var d = _productRepo.GetById(p => p.Id == id);

            //InvoiceDetails invoiceDetails = context.InvoicesDetails.FirstOrDefault(x => x.InvoiceId == d.Id);
            _productRepo.Delete(d);
            _productRepo.Save();
            //var invoice = context.Invoices.SingleOrDefault(p => p.Id == d.InvoiceId);

            return RedirectToAction("ProductList");
        }
        catch
        {
            return RedirectToAction("Productlist", new { error = "error" });
        }

    }





    public IActionResult ProductEdit(int id)
    {
        var product = _productRepo.GetById(p => p.Id == id);
        return View(product);
    }
    [HttpPost]
    public IActionResult ProductEdit(Product c,IFormFile? file)
    {
		var product = _productRepo.GetById(p => p.Id == c.Id);
		string wwwRootPath = _webHostEnvironment.WebRootPath;
		if (file != null)
		{
			string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			string productPath = Path.Combine(wwwRootPath, @"images\product");
			using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			c.ImageUrl = @"\images\product\" + filename;
		}
		
       
            product.Color = c.Color;
            product.Color = c.Color;
            product.Description = c.Description;
            product.ListPrice = c.ListPrice;
            product.Price50 = c.Price50;
            product.Price100 = c.Price100;
            product.ImageUrl=c.ImageUrl;
            _productRepo.Update(product);
            _productRepo.Save();
        return RedirectToAction("ProductList");
    
       
    }


    public async Task<IActionResult> CreateProduct(int id)
    {
        CategoryProdcut categoryproduct = new CategoryProdcut();
        categoryproduct.ListOfCategories = new List<SelectListItem>();
        var CategoryData = (List<Category>)await _CategoryRepo.GetAll();
        foreach (var x in CategoryData)
        {
            categoryproduct.ListOfCategories.Add(new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.Id)
            });
        }

        return View(categoryproduct);
    }
   
    [HttpPost]
    public IActionResult CreateProduct(CategoryProdcut cc, IFormFile? file)
    {
		Product product = new Product();
		string wwwRootPath = _webHostEnvironment.WebRootPath;
        if (file!=null)
        {
            string filename= Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);   
            string productPath= Path.Combine(wwwRootPath, @"images\product");
            using(var fileStream= new FileStream(Path.Combine(productPath, filename), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            cc.product.ImageUrl = @"\images\product\" + filename;
        }
            
            product.CategoryId = cc.product.Category.Id;
            product.Name = cc.product.Name;
            product.Color = cc.product.Color;
            product.ListPrice = cc.product.ListPrice;
            product.Price100 = cc.product.Price100;
            product.Price50 = cc.product.Price50;
            product.Description = cc.product.Description;
            product.ImageUrl =cc.product.ImageUrl;

            _productRepo.Add(product);
            _productRepo.Save();


        
        return RedirectToAction("ProductList");

    }


    public async Task<IActionResult> Test(Invoice s)
    {

        var xs = (List<Invoice>)await _invoiceRepo.GetAll();
        _invoiceRepo.LastItem(s.Id);

        var lastInvoice = (from p in xs
                           orderby p.Id descending
                           select p).FirstOrDefault();

        //List<Invoice> invoicess =context.Invoices.Last();
        ProductView productView = new ProductView();

        productView.Listofproducts = new List<SelectListItem>();
        productView.Companylist = new List<SelectListItem>();
        productView.numberOfItems = lastInvoice.NumberofItems;
        productView.invoice = s;
        productView.Listofproducts.Select(n => new SelectListItem
        {
            Value = n.Value,
            Text = n.Text
        }).ToList();
        var CompanyData = (List<CompanySupplier>)await _companySupplierRepo.GetAll();
        var ProductsData = (List<Product>)await _productRepo.GetAll();
        foreach (var x in ProductsData)
        {
            productView.Listofproducts.Add(new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.Id)
            });
        }
        foreach (var x in CompanyData)
        {
            productView.Companylist.Add(new SelectListItem
            {
                Text = x.CompanyName,
                Value = Convert.ToString(x.CSId)
            });
        }
        return View(productView);
    }
    [HttpPost]
    public IActionResult Test(ProductView a)
    {
        a.invoiceDetails.InvoiceId = a.invoice.Id;
        a.invoiceDetails.ProductId = a.Product.Id;
        a.invoiceDetails.ExpiryDate.ToString();
        _invoicedetailsRepo.Add(a.invoiceDetails);

        _invoicedetailsRepo.Save();

        ModelState.Clear();
        return RedirectToAction("Test", a.invoice);
    }
    public IActionResult InvoiceDelete(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        Invoice invoice2 = _invoiceRepo.GetById(c => c.Id == id);
        // Invoice invoice2 = context.Invoices.FirstOrDefault(c => c.Id == id);
        // Category categoryFromDb = cantext.categories.Find(id);
        //Invoice invoice2 = context.Invoices.Find(id);
        if (invoice2 == null)
        {
            return NotFound();
        }

        //Category category2 = context.categories.Where(c => c.Id == id).FirstOrDefault();


        return View("InvoiceDelete");
    }

/*
    public IActionResult p()
    {

        InvoiceDetails invoicedetails = context.InvoicesDetails.Include(p => p.CompanySupplier).FirstOrDefault(x => x.InvoiceId == s.Id);

        return RedirectToAction("Invoice");
    }*/
}

