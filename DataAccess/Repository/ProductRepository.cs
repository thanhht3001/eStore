using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddNewProduct(Product pro)
        {
            ProductDAO.Instance.AddNew(pro);
        }

        public void DeleteProduct(int productID)
        {
            ProductDAO.Instance.Delete(productID);
        }

        public Product GetProductByID(int productID)
        {
            return ProductDAO.Instance.GetProductByID(productID);
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductDAO.Instance.GetProductList();
        }

        public void UpdateProduct(Product pro)
        {
            ProductDAO.Instance.Update(pro);
        }
        public IEnumerable<Product> SearchProductByName(string productNameValue)
        {
            return ProductDAO.Instance.SearchProductByName(productNameValue);
        }

        public IEnumerable<Product> SearchProductByUnitPrice(decimal startPrice, decimal endPrice)
        {
            return ProductDAO.Instance.SearchProductByUnitPrice(startPrice, startPrice);
        }
    }
}
