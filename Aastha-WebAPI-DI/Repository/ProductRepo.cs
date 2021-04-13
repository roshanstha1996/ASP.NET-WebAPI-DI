using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepo : IProductRepo
    {
        public IEnumerable<ProductTable> GetAllProduct()
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                return entities.ProductTables.ToList();
            }
        }

        public ProductTable SaveProductDetail(ProductTable product)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                entities.ProductTables.Add(product);
                var newProduct =  entities.SaveChanges();
                return entities.ProductTables.FirstOrDefault(p => p.ProductId == newProduct);
            }
        }

        public ProductTable UpdateProduct(ProductTable product)
        {
            using(AasthaDBEntities entities= new AasthaDBEntities())
            {
                var entity = entities.ProductTables.FirstOrDefault(p => p.ProductId == product.ProductId);

                entity.CategoryId = product.CategoryId;
                entity.ProductName = product.ProductName;
                entity.Rate = product.Rate;
                entity.QuantityInStock = product.QuantityInStock;
                entity.ThresholdValue = product.ThresholdValue;
                entity.MfgDate = product.MfgDate;
                entity.ExpDate = product.ExpDate;
                entity.Remarks = product.Remarks;

                entities.SaveChanges();

                return entity ;
            }
        }

        public IEnumerable<GetSalesReportByDate_Result> GetSalesReportByDate(DateTime fromdate, DateTime todate)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                var result = entities.GetSalesReportByDate(fromdate, todate).ToList();

                if(result != null)
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
