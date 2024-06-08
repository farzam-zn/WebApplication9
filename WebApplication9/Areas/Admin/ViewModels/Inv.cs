using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Models.Models;

namespace WebApplication9.ViewModels
{
	public class Inv
	{
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

		public string Sname { get; set; }
		public CompanySupplier Supplier { get; set; }

	}
}
