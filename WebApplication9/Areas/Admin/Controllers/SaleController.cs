using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using WebApplication9.ViewModels;

namespace WebApplication9.Controllers
{
   
	public class SaleController : Controller
    {


        private readonly IInvoiceRepository _invoiceRepo;
        private readonly ICompanySupplier _companySupplierRepo;
        private readonly IInoviceDetailsRepository _invoicedetailsRepo;
        private readonly IProductReposirory _productRepo;
        private readonly ICategoryRepository _CategoryRepo;
        private readonly ISaleRepository _saleRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly ISaleInvoiceRepository _saleInvoiceRepo;   
        public SaleController(IInvoiceRepository db,ISaleInvoiceRepository saleInvoiceRepository,ICustomerRepository customerRepository, IInoviceDetailsRepository invoicedetailsRepo, ICategoryRepository categoryRepository, IProductReposirory productReposirory, ICompanySupplier companySupplier, ISaleRepository saleRepo)
        {
            _invoiceRepo = db;
            _invoicedetailsRepo = invoicedetailsRepo;
            _companySupplierRepo = companySupplier;
            _productRepo = productReposirory;
            _CategoryRepo = categoryRepository;
            _saleRepo = saleRepo;
            _saleInvoiceRepo = saleInvoiceRepository;
            _customerRepo= customerRepository;  

        }
        
        public async Task<IActionResult> Sale(SaleInvoice sa)
        {    
            var x = await _productRepo.GetAll();
            SaleViewModel saleViewModel = new SaleViewModel();
            saleViewModel.Items = new List<SelectListItem>();
            saleViewModel.saleinvoice = sa; 
            var ProductsData = (List<Product>)await _productRepo.GetAll();
            foreach (var n in ProductsData)
            {
                saleViewModel.Items.Add(new SelectListItem
                {
                    Text = n.Name,
                    Value = Convert.ToString(n.Id)
                });
            }
            saleViewModel.saleinvoice= sa;
            var products = _productRepo.GetAllProducts().ToList();
 
            products = products.Select(p => { p.Quantity = p.InvoiceDetails.Sum(s => s.Quantity); return p; }).ToList();
            return View(saleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Sale(SaleViewModel saleViewModel)
        {
            saleViewModel.sale.SaleInvoiceId = saleViewModel.saleinvoice.Id;
            saleViewModel.sale.ProductId=saleViewModel.sale.ProductId;
            saleViewModel.sale.sellQ = saleViewModel.sale.sellQ;
            saleViewModel.sale.SaleInvoiceId = saleViewModel.saleinvoice.Id;
            
            var q = _saleRepo.GET().ToList();
            var r = q.Select(p => p.product).ToList();
            SaleList saleee = new SaleList();
            Sale salr = new Sale();
            var theName = _productRepo.GetById(b => b.Id == saleViewModel.sale.ProductId);
            var ProductCate = _productRepo.GetIt().ToList();
            Product ppcc = ProductCate.FirstOrDefault(op => op.Id == saleViewModel.sale.ProductId);
            var c = (List<Product>)await _productRepo.GetAll();
            var o = c.FirstOrDefault(e => e.Id == saleViewModel.product.Id);
            var prod = ProductCate.Where(u => u.Id == saleViewModel.sale.ProductId).Select(p => new Product { Id = p.Id, CategoryId = p.CategoryId, Name = p.Name, Category = new Category { Name = p.Category.Name }, SellQuantity = ProductCate.Where(er => er.Id == saleViewModel.sale.ProductId).SelectMany(i => i.InvoiceDetails).Sum(t => t.Quantity) }).ToList();
            saleee.theName = theName.Name;
            saleee.date = DateTime.Now;
            saleee.theQ = saleViewModel.sale.sellQ;
            salr.sellQ = saleee.theQ;
            salr.TheDate = saleee.date;
            salr.ProductId = saleViewModel.sale.ProductId;
            salr.SaleInvoiceId = saleViewModel.saleinvoice.Id;
            salr.Price = saleViewModel.sale.Price;
            salr.SalePrice=saleViewModel.sale.SalePrice;
/*            salr.ssaleinvoice.SaleInvoiceNum = saleViewModel.saleinvoice.SaleInvoiceNum;
*/            var productsInvoice2 = prod.FirstOrDefault(q => q.Id == saleViewModel.sale.ProductId);
            var l = prod.FirstOrDefault(ii => ii.Id == saleViewModel.sale.ProductId);
            var t = l.SellQuantity - theName.Quantity;
            if (t > saleViewModel.sale.sellQ)
            {
                ppcc.Quantity = (ppcc.Quantity + saleViewModel.sale.sellQ);

                _saleRepo.Update(salr);
                _saleRepo.Save();
                _productRepo.Update(ppcc);
                _productRepo.Save();
                return RedirectToAction("Sale");
            }


            else

            {
                return View("SaleException");
                
            }




        }


        public IActionResult SaleList(int id)
        {
		
			var u = _saleRepo.SaleInvDetails(id).ToList();
            var z= _saleInvoiceRepo.GetById(op=>op.Id== id);
			SaleList salelist = new SaleList();
            List<Sale> ss= new List<Sale>();
            salelist.sale = ss;
			foreach (var item in u)
			{
				salelist.sale.Add(new Sale
				{
					Id = item.Id,
					product = item.product,
					sellQ = item.sellQ
                   ,SaleInvoiceId = item.SaleInvoiceId  
                  
				});

			}

			salelist.InvNum = id;
			/*var q = _saleRepo.GET().ToList();
		var r = q.Select(p => p.product).ToList();

		List<SaleList> sll = new List<SaleList>();

			foreach(var qq in q) {
				sll.Add(new SaleList
				{

					Id=qq.Id,
					theName = qq.product.Name,
					date=qq.TheDate,
					theQ=qq.sellQ,
				}
		}*/

			return View(salelist); 
        }
        [HttpPost]
		public IActionResult SaleList2(int id)
		{

			var u = _saleRepo.SaleInvDetails(id).ToList();
			var z = _saleInvoiceRepo.GetById(op => op.Id == id);
			SaleList salelist = new SaleList();
			List<Sale> ss = new List<Sale>();
			salelist.sale = ss;
			foreach (var item in u)
			{
				salelist.sale.Add(new Sale
				{
					Id = item.Id,
					product = item.product,
					sellQ = item.sellQ
				   ,
					SaleInvoiceId = item.SaleInvoiceId
				});

			}

			salelist.InvNum = id;
			/*var q = _saleRepo.GET().ToList();
		var r = q.Select(p => p.product).ToList();

		List<SaleList> sll = new List<SaleList>();

			foreach(var qq in q) {
				sll.Add(new SaleList
				{

					Id=qq.Id,
					theName = qq.product.Name,
					date=qq.TheDate,
					theQ=qq.sellQ,
				}
		}*/

			return View("salelist",salelist);
		}
        public IActionResult Delete(int id)
        {
            var item = _saleInvoiceRepo.GetById(pp=>pp.Id==id);
            return View(item);
        }
        [HttpPost]
		public IActionResult Ddelete(int id)
        {
            var listofSales = new List<Sale>();
            var th = _saleInvoiceRepo.include(id).ToList() ;
            var salinv = _saleInvoiceRepo.GetById(o=>o.Id==id);
            var ht = _saleRepo.SaleAndInv(id).ToList() ;
            var l=0;
            foreach(var item in ht)
            {
                l = l + item.sellQ;
            };
		         var d= _saleRepo.GetById(p => p.SaleInvoiceId == id);
                //var thh = _saleInvoiceRepo.Get(aa=>aa.Id == id);    
               
                //var THEQ = d.sellQ;
                var s= _productRepo.GetById(op=>op.Id==d.ProductId);
                 s.Quantity = s.Quantity - l;
                //   var theF = (s.Quantity-THEQ);
                foreach(var t in ht) {
				_saleRepo.Delete(t);
				_saleRepo.Save();
			}
            _saleInvoiceRepo.Delete(salinv);
            _saleInvoiceRepo.Save();
           
			//	s.Quantity = theF;
                _productRepo.Update(s);
                _productRepo.Save();
					//InvoiceDetails invoiceDetails = context.InvoicesDetails.FirstOrDefault(x => x.InvoiceId == d.Id);
				
				//var invoice = context.Invoices.SingleOrDefault(p => p.Id == d.InvoiceId);

				return RedirectToAction("SaleInvoice");
			
				/*catch
				{
					return RedirectToAction("SaleList", new { error = "error" });
				}*/
        }
        public IActionResult SaleEdit(int id)
        {
			var sale = _saleRepo.GetById(p => p.Id == id);
            
			return View(sale);
		}
		public IActionResult SaleDetailsDelete(int id)
		{
			var d = _saleRepo.GetById(p => p.Id == id);
            
			var a = _productRepo.GetById(aa => aa.Id == d.ProductId);
			a.Quantity = (a.Quantity - d.sellQ);
			_productRepo.Update(a);
			_productRepo.Save();
			_saleRepo.Delete(d);
			_saleRepo.Save();
			var idd = _saleInvoiceRepo.GetById(pp => pp.Id == d.SaleInvoiceId).Id;
			return RedirectToAction("SaleList", new { id = idd });
		}

		[HttpPost]
		public IActionResult SaleEdit(Sale c, int id)
		{
			var t = c.SaleInvoiceId;
			var sale = _saleRepo.GetById(p => p.Id == id);
			var s = _productRepo.GetById(op => op.Id == sale.ProductId);
			var ProductCate = _productRepo.GetIt().ToList();
			var prod = ProductCate.Where(u => u.Id == s.Id).Select(p => new Product { Id = p.Id, CategoryId = p.CategoryId, Name = p.Name, Category = new Category { Name = p.Category.Name }, SellQuantity = ProductCate.Where(er => er.Id == s.Id).SelectMany(i => i.InvoiceDetails).Sum(t => t.Quantity) }).ToList();
			var l = prod.FirstOrDefault(ii => ii.Id == s.Id);
			var tt = l.SellQuantity - s.Quantity;
            if (tt > c.sellQ) {
                s.Quantity = s.Quantity - sale.sellQ;
                s.Quantity = s.Quantity + c.sellQ;
                /*sale.sellQ =sale.sellQ+ c.sellQ;*/
                sale.sellQ = c.sellQ;

          


                _productRepo.Update(s);
                _productRepo.Save();
                //product.Color = c;
                _saleRepo.Update(sale);
                _saleRepo.Save();
                return RedirectToAction("Salelist",new { id=t });

            }
            else
            {
				return View("SaleException");
			}
       
		}
        public IActionResult SaleInvoice()
        {
           
            var q= _saleInvoiceRepo.saleInvoices().ToList();
            //var x = q.Select(a => a.Customer.Name).ToList();
            List<SaleInvoiceCustomer> saleInvoiceCustomer = new List<SaleInvoiceCustomer>();
            foreach(var item in q)
            {
				//var i = _saleRepo.Costss(item.Id).ToList();

				saleInvoiceCustomer.Add(new SaleInvoiceCustomer
                {
                    Id = item.Id,
                    customerName=item.Customer.Name,
                    Costs=item.costs
                    ,expire=DateTime.Now
                    ,SumOfCosts = _saleRepo.Costss(item.Id)
				});
            }

            return View(saleInvoiceCustomer);
        }
        public IActionResult SaleDetails(int id)
        {
            var u = _saleRepo.SaleInvDetails(id).ToList(); 
            List<SaleList> salelist = new List<SaleList>();
            foreach(var item in u){
                salelist.Add(new SaleList
                {
                    Id=item.Id,
                    theName=item.product.Name,
                    theQ=item.sellQ,
					
				});    
            
            }
			return View(salelist);
        }
        public async Task<IActionResult> SaleInvoiceCreate()
        { 
            SaleViewModel saleViewModel = new SaleViewModel();  
			saleViewModel.customerList = new List<SelectListItem>();
			var CustomerData = (List<Customer>)await _customerRepo.GetAll();
			foreach (var cus in CustomerData)
			{
				saleViewModel.customerList.Add(new SelectListItem
				{
					Text = cus.Name,
					Value = Convert.ToString(cus.Id)
				});
			}
			return View(saleViewModel);
        }
        [HttpPost]
        public IActionResult SaleInvoiceCreate(SaleViewModel saleView)
        {
            SaleViewModel ss= new SaleViewModel();
            SaleInvoice sa = new SaleInvoice();
            sa.Id = saleView.saleinvoice.Id;
            sa.CustomerId = saleView.customer.Id;
            sa.SaleInvoiceNum=saleView.saleinvoice.SaleInvoiceNum;
            
            _saleInvoiceRepo.Add(sa);
            _saleInvoiceRepo.Save();
            return RedirectToAction("Sale",sa);
        }
        public async Task<IActionResult> AddDetails(int id)
        {
            
           var theSaleInvoice = _saleInvoiceRepo.GetById(p => p.Id == id);
            Sale sa = new Sale();

			SaleViewModel saleViewModel = new SaleViewModel();
            saleViewModel.sale = sa;
			saleViewModel.Items = new List<SelectListItem>();
			saleViewModel.saleinvoice = theSaleInvoice;
            saleViewModel.sale.SaleInvoiceId = id;
			var ProductsData = (List<Product>)await _productRepo.GetAll();
			foreach (var n in ProductsData)
			{
				saleViewModel.Items.Add(new SelectListItem
				{
					Text = n.Name,
					Value = Convert.ToString(n.Id)
				});
			}
			return View(saleViewModel);
        }
        [HttpPost]
        public IActionResult AddDetails(SaleViewModel ss,int id)
        {

			var c = _saleInvoiceRepo.GetById(a => a.Id == id);
            var p = _productRepo.GetById(a=>a.Id==ss.sale.ProductId);
            p.Quantity= p.Quantity+ss.sale.sellQ;
			Sale sale = ss.sale;
            sale.SaleInvoiceId= id;
			sale.SaleInvoiceId =id;
            var xx = sale.SaleInvoiceId;
			_saleRepo.Add(sale);
			_saleRepo.Save();
            _productRepo.Update(p);
            _productRepo.Save();
			return RedirectToAction("SaleList", new {id});

        }
        
        
     

		}
        


}




















/*			


			var productss = products.FirstOrDefault(a=>a.Id==saleViewModel.product.Id);
			var x = products.SingleOrDefault(a => a.Id == saleViewModel.product.Id);

				//x.Select(p => p.Quantity).ToList();


return RedirectToAction("Sale");
}

/*[HttpPost]
public async Task<IActionResult> Sale(SaleViewModel saleView)
{
var pp = _productRepo.GetAllProducts().ToList();
var products = _productRepo.GetAllProducts().ToList();
products = products.Select(p => { p.Quantity = p.InvoiceDetails.Sum(s => s.Quantity); return p; }).ToList();
//Product prodctss=products.FirstOrDefault(p=>p.Id==saleView.product.Id);
Product product2 = pp.FirstOrDefault(a=>a.Id==saleView.product.Id);
if (product2.Quantity>saleView.product.Quantity)
{
	product2.Quantity = product2.Quantity+saleView.product.Quantity;
	//prodctss.Quantity=(prodctss.Quantity-saleView.product.Quantity);
	//saleView.x=prodctss.Quantity;

	var pr= _productRepo.Get(a=>a.Id==saleView.product.Id);
//		var IP= _invoicedetailsRepo.Get(s=>s.ProductId==saleView.product.Id);
	//IP.Quantity = prodctss.Quantity;
*//*	pr.Quantity = prodctss.Quantity;
	_productRepo.Update(pr);
	_productRepo.Save();*/
/*				_invoicedetailsRepo.Update(IP);
*//*


//foreach(var a in )










return RedirectToAction("Sale");
}
else
{
return View("SaleException");
}


}*/
/*public async Task<IActionResult> SaleChanges(Product q)
{
	var products= _productRepo.Get(a=>a.Id==q.Id);
	products.Quantity = q.Quantity;
	_productRepo.Update(products);
	_productRepo.Save();
	return View("Sale");
}*/