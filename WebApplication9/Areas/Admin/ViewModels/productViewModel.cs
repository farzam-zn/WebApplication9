namespace WebApplication9.Areas.Admin.ViewModels
{
	public class productViewModel
	{
		public int ProductId { get; set;}
		public string ProductName { get; set;}
		public string ProductDescription { get; set;}
		public int ProductCategoryId { get; set;}
		public string ProductCategoryName { get; set;}
		public string ImageUrl { get; set;}
		public double ListPrice { get; set;}
		public double PriceFor50 { get; set;} 
		public double PriceFor100 { get; set;}
		public int count { get; set;}
		public int ProductImages { get; set;}

	}
}
