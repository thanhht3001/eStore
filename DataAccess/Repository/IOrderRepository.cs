using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrderHistory(int memberId);
        void AddNewOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderID);
        //THANHHT
        Order GetOrderByID(int orderID);
        OrderDetail GetOrderDetailByID(int orderID);
    }
}
