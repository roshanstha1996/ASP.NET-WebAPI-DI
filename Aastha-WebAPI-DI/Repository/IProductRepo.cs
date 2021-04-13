using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepo
    {
        IEnumerable<ProductTable> GetAllProduct();
        ProductTable SaveProductDetail(ProductTable product);

        ProductTable UpdateProduct(ProductTable product);

        IEnumerable<GetSalesReportByDate_Result> GetSalesReportByDate(DateTime fromdate, DateTime todate);
    }
}
