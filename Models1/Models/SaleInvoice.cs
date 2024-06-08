using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class SaleInvoice
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int SaleInvoiceNum { get; set; }
		public int costs { get; set; }
		[AllowNull]
		public DateTime ExpiryDate { get; set; }	
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }	
		public ICollection<Sale> sale { get; set; }
	}
}
