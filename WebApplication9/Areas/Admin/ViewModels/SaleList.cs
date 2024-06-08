using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Models.Models;

namespace WebApplication9.ViewModels
{
	public class SaleList
	{

	
		public int Id { get; set; }
		 public List<Sale> sale { get; set; }
		public Customer customer { get; set; }
		public SaleInvoice saleinvoice { get; set; }	
		public string theName {  get; set; } 
		public int theQ {  get; set; }
		public int SumOfCost { get; set; }
		public DateTime date { get; set; }	
		public int InvNum { get; set; }	
        [AllowNull]
		public RemainingViewModel RemainingViewModel { get; set; }
        [AllowNull]

        public SaleViewModel SaleViewModel { get; set; }	
	}
}
