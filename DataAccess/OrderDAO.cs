using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        // code below here
        public IEnumerable<Order> GetOrders()
        {
            //List<Order> orders;
            try
            {
                using FStoreDBContext myContext = new FStoreDBContext();
                var orders = myContext.Orders.ToList();
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }//end GetOrders
        public void AddNewOrder(Order order)
        {
            try
            {
                Member member = new Member();
                member = GetMemberByID(member.MemberId);
                if(order.MemberId.Equals(member)) 
                { 
                    using FStoreDBContext myContext = new FStoreDBContext();
                    myContext.Orders.Add(order);
                    myContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The Member ID does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }//end AddNewOrder
        public Member GetMemberByID(int memberID)
        {
            Member member = null;
            try
            {
                using var context = new FStoreDBContext();
                member = context.Members.SingleOrDefault(m => m.MemberId == memberID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }        
        public IEnumerable<Order> GetOrderHistory(int memberId)
        {
            try
            {
                FStoreDBContext dBContext = new FStoreDBContext();
                var orders = dBContext.Orders.Where(o => o.MemberId == memberId).ToList();
                return orders;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //THANHHT
        public Order GetOrderByID(int orderID)
        {
            Order order = null;
            try
            {
                using var context = new FStoreDBContext();
                order = context.Orders.SingleOrDefault(m => m.OrderId == orderID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public void UpdateOrder(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    using var context = new FStoreDBContext();
                    context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order has not existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteOrder(int orderID)
        {
            try
            {
                Order order = GetOrderByID(orderID);
                if (order != null)
                {
                    using var context = new FStoreDBContext();
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

