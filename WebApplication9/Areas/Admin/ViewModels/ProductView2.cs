using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using System.ComponentModel;

namespace WebApplication9.ViewModels
{
	public class ProductView2
	{
		[DisplayName("Product")]
		public Product Product { get; set; }
		public Invoice invoice { get; set; }
		public List<InvoiceDetails> invoiceDetails { get; set; }
		public List<InvoiceDetails> InvoiceDetails { get; internal set; }
		public CompanySupplier companySupplier { get; set; }
		public int numberOfItems { get; set; }
		public List<SelectListItem> SupplierNameList { get; set; }
		public List<SelectListItem> Listofproducts { get; set; }
		public List<SelectListItem> Companylist { get; set; }
		public int LastInvoiceToChangeid { get; set; }
	}
}
