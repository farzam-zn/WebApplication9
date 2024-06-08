using Models.Models;

namespace WebApplication9.ViewModels
{
    public class ProductsInvoiceDetails
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public InvoiceDetails invoiceDetails { get; set; }
        public int Quantity { get; set; }

    }
}