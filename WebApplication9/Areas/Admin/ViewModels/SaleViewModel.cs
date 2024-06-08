using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace WebApplication9.ViewModels
{
	public class SaleViewModel
	{
        [AllowNull]

        public Product product { get; set; }
        [AllowNull]
        public Sale sale { get; set; }
        public Customer customer { get; set; }
        public SaleInvoice saleinvoice { get; set; }
        public List<SelectListItem> customerList { get; set; }  
        public int QQ {  get; set; }
		public List<SelectListItem> Items { get; set; }
        [AllowNull]

        public DateTime DateTime { get; set; }= DateTime.Now;
        [AllowNull]

        public List<int> ss {  get; set; }	
	}
}
