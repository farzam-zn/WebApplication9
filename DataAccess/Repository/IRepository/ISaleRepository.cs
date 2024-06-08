using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ISaleRepository:IRepository<Sale>
    {

        void Update(Sale sale);
        void Save();
        public IEnumerable<Sale> GET();
            public IEnumerable<Sale> SaleInvDetails(int id);
        public IEnumerable<Sale> SaleAndInv (int id );
        public int Costss(int id);
            }
}
