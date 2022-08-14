using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private FStoreDBContext _context = new FStoreDBContext();

        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance { 
            get 
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
                
            } 
        }
        public OrderDetail GetOrderDetailByID(int orderDetailID)
        {
            OrderDetail orderDetai = null;
            try
            {

                orderDetai = _context.OrderDetails.SingleOrDefault(p => p.OrderId == orderDetailID);
                return orderDetai;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
