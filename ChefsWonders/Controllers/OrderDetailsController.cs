using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChefsWonders.Models;
using ChefsWonders.ViewModels;

namespace ChefsWonders.Controllers
{
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<OrderCommonViewModel> OrderItemlist = new List<OrderCommonViewModel>();
            
            var orderlist = (from ord in db.OrderDetails 
                            join fooditem in db.FoodItems on ord.FoodItemID equals fooditem.FoodItemID
                            join fct in db.FoodCategories on fooditem.FoodCategoryID equals fct.FoodCategoryID
                            join ftp in db.FoodTypes on fooditem.FoodTypeID equals ftp.FoodTypeID
                            where ord.OrderID == id
                            orderby fct.FoodCategoryName
                            select new { ord.OrderID, ord.OrderDetailsID, ord.ChefID, ord.statuscode ,fct.FoodCategoryName, ftp.FoodTypeName, fooditem.FoodItemName, fooditem.Price, ord.Quantity }).ToList();
            var orderpreparedcnt = 0;
            var Assignedcnt = 0;

            foreach (var item in orderlist)
            {

                OrderCommonViewModel ocvm = new OrderCommonViewModel(); // ViewModel
                ocvm.FoodCategoryName = item.FoodCategoryName;
                ocvm.FoodTypeName = item.FoodTypeName;
                ocvm.FoodItemName = item.FoodItemName;
                ocvm.Price = item.Price;
                ocvm.Quantity = item.Quantity;
                ocvm.OrderDetailsID = item.OrderDetailsID;
                ocvm.ChefID = item.ChefID;
                ocvm.StatusCode = item.statuscode;

                if(item.statuscode == 4)
                {                    
                    Assignedcnt++;                   
                }
                else if(item.statuscode ==6)
                {
                    Assignedcnt++;
                    orderpreparedcnt++;                    
                }
                else
                {
                }

                if (orderlist.Count() == Assignedcnt)
                {
                    ViewBag.Assigned = true;
                }
                else
                {
                    ViewBag.Assigned = false;
                }

                if (orderlist.Count() == orderpreparedcnt)
                {
                    ViewBag.OrderPrepared = true;
                }
                else
                {
                    ViewBag.OrderPrepared = false;
                }

                OrderItemlist.Add(ocvm);

            }

            if (OrderItemlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Chefs = db.Chefs.ToList();
            var setStaus = new List<int>() { 4,5,6 };
            ViewBag.StatusMaster = db.StatusMasters.Where(x => setStaus.Contains(x.statuscode)).ToList();
            ViewBag.OrderID =id;
            return View(OrderItemlist.ToList());
        }


        public ActionResult GetBill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<OrderCommonViewModel> OrderItemlist = new List<OrderCommonViewModel>();

            var orderlist = (from ord in db.OrderDetails
                             join fooditem in db.FoodItems on ord.FoodItemID equals fooditem.FoodItemID
                             join fct in db.FoodCategories on fooditem.FoodCategoryID equals fct.FoodCategoryID
                             join ftp in db.FoodTypes on fooditem.FoodTypeID equals ftp.FoodTypeID
                             where ord.OrderID == id
                             orderby fct.FoodCategoryName
                             select new { ord.OrderID, ord.OrderDetailsID, fct.FoodCategoryName, ftp.FoodTypeName, fooditem.FoodItemName, fooditem.Price, ord.Quantity }).ToList();

            foreach (var item in orderlist)
            {
                OrderCommonViewModel ocvm = new OrderCommonViewModel(); // ViewModel
                ocvm.FoodCategoryName = item.FoodCategoryName;
                ocvm.FoodTypeName = item.FoodTypeName;
                ocvm.FoodItemName = item.FoodItemName;
                ocvm.Price = item.Price;
                ocvm.Quantity = item.Quantity;
                ocvm.OrderDetailsID = item.OrderDetailsID;                
                OrderItemlist.Add(ocvm);
            }

            if (OrderItemlist == null)
            {
                return HttpNotFound();
            }            
            
            ViewBag.OrderID = id;
            return View(OrderItemlist.ToList());
        }



        public JsonResult AssignChefs(List<OrderCommonViewModel> orderItemsChefs)
        {
            
            if (orderItemsChefs == null)
            {
                orderItemsChefs = new List<OrderCommonViewModel>();
            }

            int orderupdate = 0;
            int orderid = 0;

            foreach (OrderCommonViewModel orderitem in orderItemsChefs)
            {
                OrderDetails ord = new OrderDetails();
                ord = db.OrderDetails.Where(orddetails => orddetails.OrderDetailsID == orderitem.OrderDetailsID).FirstOrDefault();                
                ord.statuscode = 4;
                ord.ChefID = orderitem.ChefID;
                
                orderupdate++;
                orderid = orderitem.OrderID;                

                db.SaveChanges();
            }

            if (orderItemsChefs.Count() > 0)
            {
                Order order = new Order();
                order = db.Orders.Where(orders => orders.OrderID == orderid).SingleOrDefault();
                order.statuscode = 8;
                db.SaveChanges();
            }

            return Json("");
        }

        public JsonResult UpdateOrder(List<OrderCommonViewModel> updateorderstatus)
        {

            if (updateorderstatus == null)
            {
                updateorderstatus = new List<OrderCommonViewModel>();
            }

            int orderupdate = 0;
            int orderid = 0;
            foreach (OrderCommonViewModel orderitem in updateorderstatus)
            {
                OrderDetails ord = new OrderDetails();
                ord = db.OrderDetails.Where(orddetails => orddetails.OrderDetailsID == orderitem.OrderDetailsID).FirstOrDefault();
                ord.statuscode = orderitem.StatusCode;
                
                if(orderitem.StatusCode == 6)
                {
                    orderupdate++;
                    orderid = orderitem.OrderID;
                }

                db.SaveChanges();
            }

            if(orderupdate == updateorderstatus.Count())
            {
                Order order = new Order();
                order = db.Orders.Where(orders => orders.OrderID == orderid).SingleOrDefault();
                order.statuscode = 6;
                db.SaveChanges();                
            }
            
            
            
            return Json("");
        }


        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailsID,Quantity,OrderID")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailsID,Quantity,OrderID")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderDetails.OrderID);
            return View(orderDetails);
        }

       

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
