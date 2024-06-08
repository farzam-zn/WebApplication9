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
	public class CompanySupplierRepository:Repository<CompanySupplier>,ICompanySupplier
	{

		private readonly ApplicationDbContext _db;

		public CompanySupplierRepository(ApplicationDbContext db) :base (db)
		{
			_db = db;
		}
		public void Save()
		{
			_db.SaveChanges();
		}
			public void Update(CompanySupplier companySupplier)
		{
			_db.CompanySuppliers.Update(companySupplier);
		}
			}
}
