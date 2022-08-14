using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace eStore.Controllers
{
    public class AdminController : Controller
    {
        IMemberRepository memberRepository = new MemberRepository();
        IProductRepository productRepository = new ProductRepository();
         IOrderRepository orderRepository = new OrderRepository();
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
        // GET: AdminController/Products
        public ActionResult Products()
        {
            int? memberID = HttpContext.Session.GetInt32("MemberID");
            if (memberID != null)
            {
                var products = productRepository.GetProducts().ToList();
                return View(products);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        // GET: AdminController/NewProduct
        public ActionResult NewProduct()
        {
            return View();
        }
        // POST: AdminController/NewProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProduct(Product pro)
        {
            try
            {
                int? memberID = HttpContext.Session.GetInt32("MemberID");
                if (memberID != null)
                {
                    productRepository.AddNewProduct(pro);
                }
                return RedirectToAction(nameof(Products));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pro);
            }
        }
        // GET: AdminController/ProductDetails/5
        public ActionResult ProductDetails(int? id)
        {
            
            if (id == null )
            {
                return NotFound();
            }
            
            var pro = productRepository.GetProductByID(id.Value);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        // GET: AdminController/DeleteProduct/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pro = productRepository.GetProductByID(id.Value);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        // POST: AdminController/DeleteProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                productRepository.DeleteProduct(id);
                return RedirectToAction(nameof(Products));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AdminController/EditProduct/5
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pro = productRepository.GetProductByID(id.Value);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        // POST: AdminController/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id, Product pro)
        {
            try
            {
                if (id != pro.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    productRepository.UpdateProduct(pro);
                }
                return RedirectToAction(nameof(Products));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Orders()
        {
            int? memberID = HttpContext.Session.GetInt32("MemberID");
            if (memberID != null)
            {
                var orders = orderRepository.GetOrders().OrderBy(p => p.OrderId).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        // GET: AdminController/NewOrder
        public ActionResult NewOrder()
        {
            return View();
        }
        // POST: AdminController/NewOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(Order order)
        {
            try
            {
                
                if ( ModelState.IsValid)
                {
                    orderRepository.AddNewOrder(order);
                }
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }
        public ActionResult SearchProducts(string productNameValue)
        {
            
                if (productNameValue != null)
                {
                    var products = productRepository.SearchProductByName(productNameValue).ToList();
                    return View(products);
                }
                else
                {
                    return RedirectToAction(nameof(Products));
                }
            
        }
        public ActionResult SearchProductByUnitPrice(decimal from, decimal to)
        {
            {

                if (from != null && to != null)
                {
                    var products = productRepository.SearchProductByUnitPrice(from, to);
                    return View(products);
                }
                else
                {
                    return RedirectToAction(nameof(Products));
                }

            }

        }
        //public ActionResult Orders()
        //{
        //    int? memberID = HttpContext.Session.GetInt32("MemberID");
        //    if (memberID != null)
        //    {
        //        var orders = orderRepository.GetOrders().OrderBy(p => p.OrderId).ToList();
        //        return View(orders);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(Index), "Home");
        //    }
        //}
        // GET: AdminController/NewOrder
        //public ActionResult NewOrder()
        //{
        //    return View();
        //}

        // POST: AdminController/NewOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
       /* public ActionResult NewOrder(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepository.AddNewOrder(order);
                }
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }*/
        
        //public ActionResult SearchProducts(string productNameValue)
        //{
            
        //        if (productNameValue != null)
        //        {
        //            var products = productRepository.SearchProductByName(productNameValue).ToList();
        //            return View(products);
        //        }
        //        else
        //        {
        //            return RedirectToAction(nameof(Products));
        //        }
            
        //}

        //THANHHT
        // GET: AdminController/EditOrder/5
        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: AdminController/EditOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(int id, Order order)
        {
            try
            {
                if (id != order.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    orderRepository.UpdateOrder(order);
                }
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        // GET: AdminController/Delete/5
        // GET: AdminController/ProductDetails/5
        public ActionResult OrderDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepository.GetOrderDetailByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // GET: AdminController/DeleteProduct/5
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: AdminController/DeleteProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                orderRepository.DeleteOrder(id);
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
