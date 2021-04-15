using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SalesRepo : ISalesRepo
    {
        public SalesCodeTable SaveSalesCodeDetail(SalesCodeTable salesCode)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                entities.SalesCodeTables.Add(salesCode);
                entities.SaveChanges();
                return entities.SalesCodeTables.FirstOrDefault(p => p.SalesCodeId == salesCode.SalesCodeId);
            }
        }

        public SalesTable SaveSalesDetail(SalesTable sales)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                entities.SalesTables.Add(sales);
                entities.SaveChanges();
                return entities.SalesTables.FirstOrDefault(p => p.SalesId == sales.SalesId);
            }
        }

        public IList<GetSalesReportByDate_Result> GetSalesReportByDate(DateTime? fromdate, DateTime? todate)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                var result = entities.GetSalesReportByDate(fromdate, todate).ToList();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<GetSalesReportByDate_Result>();
                }
            }
        }
    }
}
