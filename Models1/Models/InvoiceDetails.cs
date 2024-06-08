using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.Models
{
	public class InvoiceDetails
	{
		[Key]
		public int Id { get; set; }

		public int  InvoiceId { get; set; }

		public Product Product { get; set; }


		public int ProductId { get; set; }
		[AllowNull]

		public int Quantity { get; set; }
		[AllowNull]
		public double Price { get; set; }
		[AllowNull]
		public double SalePrice { get; set; }
		/*[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]*/
		
		public DateTime ExpiryDate { get; set; }
		//
		//public CompanySupplier companySupplier { get; set; }
		public Invoice Invoice { get; set; } //InvoiceId,ProductId,Price,SalePrice,CompanyName,ExpiryDate

	}
}
