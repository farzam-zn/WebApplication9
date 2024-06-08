using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query;
using Models.Models;

namespace WebApplication9.ViewModels
{
    public class RemainingViewModel
    {
        //public int ProductId { get; set; }
        [AllowNull]

        public int Id { get; set; }
        [AllowNull]

        public int CategoryId { get; set; }
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
		[AllowNull]
		public Product products { get; set; }
        [AllowNull]

		public Category Category { get; set; }
        [AllowNull]
        public double ListPrice { get; set; }
        [AllowNull]
        public double Price100 { get; set; }
        [AllowNull]
        public double Price50 { get; set;}
        [AllowNull]
        public DateTime DateTime { get; set; }
        [AllowNull]

        public SaleViewModel Sale { get; set; }
        [AllowNull]

        public int sellQu { get; set; }
        [AllowNull]

        public int SubCategoryId { get; set; }
	}
}
