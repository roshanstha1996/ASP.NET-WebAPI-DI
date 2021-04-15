using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BillRepo : IBillRepo
    {
        public BillTable SaveBillDetail(BillTable bill)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                entities.BillTables.Add(bill);
                var newBillId = entities.SaveChanges();
                return entities.BillTables.FirstOrDefault(p => p.BillId == newBillId);
            }
        }
    }
}
