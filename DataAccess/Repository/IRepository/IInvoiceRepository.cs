using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IInvoiceRepository:IRepository<Invoice>
	{

		void Update(Invoice invoice);
		void Save();
		void LastItem(int invoiceid);
		IEnumerable<Invoice> Convert();
	}
}
