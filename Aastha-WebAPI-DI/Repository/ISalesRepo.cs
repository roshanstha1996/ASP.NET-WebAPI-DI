using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ISalesRepo
    {
        //IEnumerable<ProductTable> GetAllProduct();
        SalesCodeTable SaveSalesCodeDetail(SalesCodeTable salesCode);
        SalesTable SaveSalesDetail(SalesTable sales);
        IList<GetSalesReportByDate_Result> GetSalesReportByDate(DateTime? fromdate, DateTime? todate);
    }
}
