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
	public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
	{
		private readonly ApplicationDbContext _db;
		public InvoiceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void LastItem(int invoiceid)
		{
			var x = _db.Invoices.FirstOrDefault(a => a.Id == invoiceid);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Invoice invoice)
		{
			_db.Invoices.Update(invoice);
		}


		public IEnumerable<Invoice> Convert()
		{
			return _db.Invoices.Include(a => a.CompanySupplier).ToList();
		}
	}

}
/*var lastInvoice = (from p in context.Invoices
                   orderby p.Id descending
                   select p).FirstOrDefault();*/