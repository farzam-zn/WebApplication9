using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IInoviceDetailsRepository:IRepository<InvoiceDetails>
	{

		void Update(InvoiceDetails invoiceDetails);
		void find(Invoice s);
		void Save();
		IEnumerable<InvoiceDetails> include(int id);
		
	}
}
