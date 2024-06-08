using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Repository
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        private readonly ApplicationDbContext _db;
        public SaleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Sale sale)
        {
            _db.sales.Update(sale);
        }
        public void Save()
        {
            _db.SaveChanges();
        }


        public IEnumerable<Sale> GET()
        {
            return _db.sales.Include(a => a.product).ToList();

        }


		public IEnumerable<Sale> SaleInvDetails(int id)
		{
			return _db.sales.Include(p => p.product).Where(a => a.SaleInvoiceId == id);
		}

        public IEnumerable<Sale> SaleAndInv(int id)
        {
            return _db.sales.Include(u => u.saleinvoice).Where(a=>a.SaleInvoiceId== id).ToList();
        }
  

		public int Costss(int id)
		{
			 return _db.sales.Where(a => a.SaleInvoiceId == id).Sum(o => o.SalePrice);
		}
	}
	}
