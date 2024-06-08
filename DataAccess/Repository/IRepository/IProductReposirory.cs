using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IProductReposirory: IRepository<Product>
	{
		void Update(Product product);
		void Save();
		IEnumerable<Product> GetAllProducts();

		IEnumerable<Product> GetProductsInclude();
		IEnumerable<Product> ProductsGroupBy();
		IEnumerable<Product> GetIt();
		IEnumerable<Product> Cost(int id);
		IEnumerable<Product> Getp(int id);
	}
}
