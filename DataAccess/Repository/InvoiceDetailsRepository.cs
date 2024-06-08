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
	public class InvoiceDetailsRepository : Repository<InvoiceDetails>, IInoviceDetailsRepository
	{
		private readonly ApplicationDbContext _db;
		public InvoiceDetailsRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Save()
		{
			_db.SaveChanges();
		}
		public void Update(InvoiceDetails invoiceDetails) { _db.InvoicesDetails.Update(invoiceDetails); }
		public void find(Invoice s)
		{
			_db.InvoicesDetails.Where(x => x.InvoiceId == s.Id).Select(p => p.Product);

		}

		public IEnumerable<InvoiceDetails> include(int id)
		{
			return _db.InvoicesDetails.Include(p => p.Product).Where(a => a.InvoiceId == id);
		}

		//public IEnumerable<InvoiceDetails> Sumision()
		//{
		//	/*var a = _db.InvoicesDetails;
		//	var aa = _db.InvoicesDetails.Sum(s=> s.Id == a.Where(p=>p.ProductId);*/


		//	var details = _db.InvoicesDetails.ToList();
		//	var products = _db.Products.ToList();
		//	foreach (var product in products)
		//	{
		//		var aa = details.Where(a => a.ProductId == product.Id).Sum(s => s.Quantity);
		//		product.Quantity = aa;
		//	}
		//	return (products);
		//}

		/*	InvoiceDetails IInoviceDetailsRepository.find(Invoice s)
            {
                throw new NotImplementedException();
            }*/
	}
}
