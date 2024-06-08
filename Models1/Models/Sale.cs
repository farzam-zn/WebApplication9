using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Sale
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id {  get; set; }
        public int ProductId { get; set; }  
        public int sellQ {  get; set; }
        public Product product { get; set; }
        public DateTime TheDate { get; set; }   
        public int SaleInvoiceId { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public int SumOfCosts { get; set; }
        public SaleInvoice saleinvoice { get; set; }  
    }
}
