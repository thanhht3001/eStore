using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private FStoreDBContext _context = new FStoreDBContext();
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }


        public IEnumerable<Product> GetProductList()
        {
            try
            {
                var products = _context.Products.ToList();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Product GetProductByID(int productID)
        {
            Product pro = null;
            try
            {
                
                pro = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                return pro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void AddNew(Product pro)
        {
            try
            {
                Product _pro = GetProductByID(pro.ProductId);
                if (_pro == null)
                {
                    
                    _context.Products.Add(pro);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product has already existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int productID)
        {
            try
            {
                Product pro = GetProductByID(productID);
                if (pro != null)
                {
                   
                    _context.Products.Remove(pro);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Product pro)
        {
            try
            {
                Product _pro = GetProductByID(pro.ProductId);
                if (_pro != null)
                {
                    using var context = new FStoreDBContext();
                    context.Entry<Product>(pro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member has not existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> SearchProductByName(string productNameValue)
        {
            try
            {
                using FStoreDBContext dBContext = new FStoreDBContext();
                var products = dBContext.Products.Where(p => p.ProductName == productNameValue);
                return products.OrderByDescending(p => p.ProductName).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public IEnumerable<Product> SearchProductByUnitPrice(decimal startPrice, decimal endPrice)
        {

            try
            {
                using FStoreDBContext dBContext = new FStoreDBContext();
                if (startPrice > endPrice)
                {
                    var products = dBContext.Products.Where(p => p.UnitPrice >= endPrice && p.UnitPrice <= startPrice);
                    return products.OrderByDescending(p => p.UnitPrice).ToList();
                }
                else if (endPrice < startPrice)
                {
                    var products = dBContext.Products.Where(p => p.UnitPrice >= startPrice && p.UnitPrice <= endPrice).ToList();
                    return products;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;


        }

    }
}
