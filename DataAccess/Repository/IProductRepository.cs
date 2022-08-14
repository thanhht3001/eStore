using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int productID);
        void AddNewProduct(Product pro);
        void UpdateProduct(Product pro);
        void DeleteProduct(int productID);
        IEnumerable<Product> SearchProductByName(string productNameValue);
        IEnumerable<Product> SearchProductByUnitPrice(decimal startPrice, decimal endPrice);
    }
}
