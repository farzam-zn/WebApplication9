using Models.Models;

namespace WebApplication9.ViewModels
{
    public class InvoiceDetailsList
    {
        public int InvoiceId { get; set; }
        public int InvoiceDetailsId { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
        public string PName { get; set; }
        public CompanySupplier companySupplier { get; set; }
    }
}
