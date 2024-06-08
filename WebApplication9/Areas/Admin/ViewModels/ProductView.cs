using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace WebApplication9.ViewModels
{
    public class ProductView
    {
        [DisplayName("Product")]
        public Product Product { get; set; }
        public Invoice invoice { get; set; }
        public InvoiceDetails invoiceDetails { get; set; }

       // public CompanySupplier companySupplier { get; set; }
        public Inv inv { get; set; }
        public int numberOfItems { get; set; }
        public List<SelectListItem> SupplierNameList { get; set; }
        public List<SelectListItem> Listofproducts { get; set; }
        public List<SelectListItem> Companylist { get; set; }
        public int LastInvoiceToChangeid { get; set; }
    }
}
