using Models.Models;

namespace WebApplication9.ViewModels
{
	public class SaleInvoiceCustomer
	{

		public int Id { get; set; }	
		public string customerName { get; set; }
		 public int Costs { get; set; }	
		public Customer Customer { get; set; }
		public SaleInvoice saleinvoice { get; set; }
		public int SumOfCosts { get; set; }
		public DateTime expire { get; set; }
	}
}
