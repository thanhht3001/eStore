using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddNewOrder(Order order)
        {
            OrderDAO.Instance.AddNewOrder(order);
        }

        public void DeleteOrder(int orderID)
        {
            OrderDAO.Instance.DeleteOrder(orderID);
        }

        public IEnumerable<Order> GetOrderHistory(int memberId)
        {
            return OrderDAO.Instance.GetOrderHistory(memberId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return OrderDAO.Instance.GetOrders();
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.Instance.UpdateOrder(order);
        }
        //THANHHT
        public Order GetOrderByID(int orderID)
        {
            return OrderDAO.Instance.GetOrderByID(orderID);
        }
        public OrderDetail GetOrderDetailByID(int orderID)
        {
            return OrderDetailDAO.Instance.GetOrderDetailByID(orderID);
        }

        
    }
}
