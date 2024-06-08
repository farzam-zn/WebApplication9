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
	public class SaleInvoiceRepository: Repository<SaleInvoice>,ISaleInvoiceRepository
	{
		private readonly ApplicationDbContext _db;
		public SaleInvoiceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public IEnumerable<SaleInvoice> saleInvoices()
		{
			return _db.saleInvoices.Include(a=>a.Customer).ToList();
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(SaleInvoice saleInvoice)
		{
			
			_db.saleInvoices.Update(saleInvoice);
		}
		public IEnumerable<SaleInvoice> include(int id)
		{
			return _db.saleInvoices.Include(_a => _a.sale).Where(sa => sa.Id==id);
		}
	}
}
