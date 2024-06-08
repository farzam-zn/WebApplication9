using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
	public class CompanySupplier
	{
		[Key]
		public int CSId { get; set; }
		public string SupplierName { get; set; }
		
		public string CompanyName { get; set; }
		public ICollection<Invoice> Invoice { get; set; }
	}
}
