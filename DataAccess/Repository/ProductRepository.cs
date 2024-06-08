using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductReposirory
	{
		private readonly ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Product product)
		{
			_db.Products.Update(product);
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _db.Products.Include(p => p.InvoiceDetails);
		}

	/*	var query = _productRepo
		.Include(p => p.InvoiceDetails).GroupBy(a => new { a.Id, a.Name, CategoryName = a.Category.Name, a.Color }).Select(a => new Product
		{
			Id = a.Key.Id,
			Name = a.Key.Name,
			Category = new Category
			{
				Name = a.Key.CategoryName,
			},
			Color = a.Key.Color,
			Quantity = a.SelectMany(p => p.InvoiceDetails).Sum(x => x.Quantity)


		}).ToList();
*/
		public IEnumerable<Product> GetProductsInclude()
		{
			return _db.Products.Include(p => p.InvoiceDetails);
		}
		public IEnumerable<Product> ProductsGroupBy()
		{
			return (List<Product>)_db.Products.Include(_p => _p.InvoiceDetails).GroupBy(a => new { a.Id, a.Name, CategoryName = a.Category.Name, a.Color });
		}

		public IEnumerable<Product> GetIt()
		{
			return _db.Products.Include(o => o.Category).Include(a=>a.InvoiceDetails);
		}
		public IEnumerable<Product> Cost(int id) {
		return _db.Products.Include(u=>u.InvoiceDetails).Where(w=>w.Id == id);
		}
	 

		public IEnumerable<Product> Getp(int id)
		{
			return _db.Products.Where(p=>p.Id == id);	
		}
	}
}
