using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace Models.Models
{
	public class Invoice
	{


		[Key]
		public int Id { get; set; }
		public string InvoiceNumber { get; set; }
		public int NumberofItems { get; set; }
		public int? UserId { get; set; }
		[AllowNull]
		public int SupplierId { get; set; }//supplier name

		[AllowNull]
		public DateTime CreationDate { get; set; }

		[AllowNull]
		public double Costs { get; set; }

		[AllowNull]
		public int PaymentId { get; set; }

		[AllowNull]

		public ICollection<InvoiceDetails> InvoiceDetails { get; set; }
		public CompanySupplier CompanySupplier { get; set; }
	}
}
