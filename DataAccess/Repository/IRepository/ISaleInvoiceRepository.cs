using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
	public interface ISaleInvoiceRepository: IRepository<SaleInvoice>
	{
		void Update(SaleInvoice saleInvoice);
		void Save();
		public IEnumerable<SaleInvoice> saleInvoices();


		public IEnumerable<SaleInvoice> include(int id);

	}
}
