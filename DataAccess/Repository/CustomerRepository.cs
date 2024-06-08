using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.Models;

namespace DataAccess.Repository
{
	public class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		private readonly ApplicationDbContext _db;
		public CustomerRepository(ApplicationDbContext db) : base(db)
		{
			_db=db;
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Customer customer)
		{
			_db.customer.Update(customer);
		}

	
	}
}
