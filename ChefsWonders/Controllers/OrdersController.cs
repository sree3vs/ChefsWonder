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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.statusMaster);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {

            List<OrderCommonViewModel> OrderItemlist = new List<OrderCommonViewModel>();
            var customerlist = (from fooditem in db.FoodItems
                                join fct in db.FoodCategories on fooditem.FoodCategoryID equals fct.FoodCategoryID
                                join ftp in db.FoodTypes on fooditem.FoodTypeID equals ftp.FoodTypeID
                                orderby fct.FoodCategoryName
                                select new { fct.FoodCategoryName, ftp.FoodTypeName, fooditem.FoodItemName, fooditem.Price }).ToList();

            foreach (var item in customerlist)
            {

                OrderCommonViewModel ocvm = new OrderCommonViewModel(); // ViewModel
                ocvm.FoodCategoryName = item.FoodCategoryName;
                ocvm.FoodTypeName = item.FoodTypeName;
                ocvm.FoodItemName = item.FoodItemName;
                ocvm.Price = item.Price;
                ocvm.Quantity = 1;

                OrderItemlist.Add(ocvm);

            }

            return View(OrderItemlist);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public ActionResult Create([Bind(Include = "OrderID,OrderDate,statuscode")] OrderCommonViewModel Orders)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCommonViewModel Orders)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }


            return View(Orders.Order);
        }


        public JsonResult InsertDetails(List<OrderCommonViewModel> items)
        {

            if (items == null)
            {
                items = new List<OrderCommonViewModel>();
            }

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.statuscode = 3;
            db.Orders.Add(order);
            db.SaveChanges();
            int orderid = order.OrderID;
            foreach (OrderCommonViewModel customer in items)
            {
                OrderDetails ord = new OrderDetails();
                ord.FoodItemID = db.FoodItems.SingleOrDefault(c => c.FoodItemName == customer.FoodItemName.Trim()).FoodItemID;
                ord.Quantity = customer.Quantity;
                ord.OrderID = orderid;
                ord.statuscode = 5;
                db.OrderDetails.Add(ord);
            }
            int insertedRecords = db.SaveChanges();
            return Json(insertedRecords);
        }
        //[HttpPost]        
        //public ActionResult Create(List<OrderCommonViewModel> Orders)
        //{
        //    Order order = new Order();
        //    if (ModelState.IsValid)
        //    {

        //        order.OrderDate = DateTime.Now;
        //        order.statuscode = 3;
        //        int ordid = order.OrderID;
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", order.statusMaster.status);
        //    return View(order);
        //}
        // GET: Orders/CreateOrder
        public ActionResult CreateOrder()
        {

            List<OrderCommonViewModelClass> OrderItemlist = new List<OrderCommonViewModelClass>();
            var customerlist = (from fooditem in db.FoodItems
                                join fct in db.FoodCategories on fooditem.FoodCategoryID equals fct.FoodCategoryID
                                join ftp in db.FoodTypes on fooditem.FoodTypeID equals ftp.FoodTypeID
                                orderby fct.FoodCategoryName
                                select new { fct.FoodCategoryName, ftp.FoodTypeName, fooditem.FoodItemName, fooditem.Price }).ToList();

            foreach (var item in customerlist)
            {

                OrderCommonViewModelClass ocvm = new OrderCommonViewModelClass();  // ViewModel


                ocvm.FoodItem.foodCategory.FoodCategoryName = item.FoodCategoryName;
                ocvm.FoodItem.foodType.FoodTypeName = item.FoodTypeName;
                ocvm.FoodItem.FoodItemName = item.FoodItemName;
                ocvm.FoodItem.Price = item.Price;
                ocvm.Quantity = 1;

                OrderItemlist.Add(ocvm);

            }

            return View(OrderItemlist);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include = "OrderID,OrderDate,statuscode")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", order.statuscode);
            return View(order);
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", order.statuscode);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,statuscode")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", order.statuscode);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
