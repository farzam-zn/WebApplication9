using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.Models
{
	public class Product
	{
		//public int ProductId { get; set; }
		
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public ICollection<Sale> sale { get; set; }	
		public string Name { get; set; }
		[AllowNull]
		public double Price { get; set; }
		[AllowNull]
		public int Quantity { get; set; }
        [AllowNull]
        public int RemainingQuantity { get; set; }
        [AllowNull]
        public int SellQuantity { get; set; }
		[AllowNull]
		public string Color { get; set; }
		[Display(Name="List Price")]
		[Range(1,1000)]
		public double ListPrice {  get; set; }


		[Display(Name = "Price for 50+")]
		[Range(1, 1000)]
		public double Price50 { get; set; }

		[Display(Name = "Price for 100+")]
		[Range(1, 1000)]
		public double Price100 { get; set; }

		[AllowNull]
		public int SubCategoryId { get; set; }
		[AllowNull]
		public string Description { get; set; }
		
		public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }
	}
}
