using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChefsWonders.Models;

namespace ChefsWonders.Controllers
{
    public class FoodItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FoodItems
        public ActionResult Index()
        {
            var foodItems = db.FoodItems.Include(f => f.foodCategory).Include(f => f.foodType);
            return View(foodItems.ToList());
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            ViewBag.FoodCategoryID = new SelectList(db.FoodCategories, "FoodCategoryID", "FoodCategoryName");
            ViewBag.FoodTypeID = new SelectList(db.FoodTypes, "FoodTypeID", "FoodTypeName");
            return View();
        }

        // POST: FoodItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodItemID,FoodItemName,Price,FoodTypeID,FoodCategoryID")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(foodItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodCategoryID = new SelectList(db.FoodCategories, "FoodCategoryID", "FoodCategoryName", foodItem.FoodCategoryID);
            ViewBag.FoodTypeID = new SelectList(db.FoodTypes, "FoodTypeID", "FoodTypeName", foodItem.FoodTypeID);
            return View(foodItem);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodCategoryID = new SelectList(db.FoodCategories, "FoodCategoryID", "FoodCategoryName", foodItem.FoodCategoryID);
            ViewBag.FoodTypeID = new SelectList(db.FoodTypes, "FoodTypeID", "FoodTypeName", foodItem.FoodTypeID);
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodItemID,FoodItemName,Price,FoodTypeID,FoodCategoryID")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodCategoryID = new SelectList(db.FoodCategories, "FoodCategoryID", "FoodCategoryName", foodItem.FoodCategoryID);
            ViewBag.FoodTypeID = new SelectList(db.FoodTypes, "FoodTypeID", "FoodTypeName", foodItem.FoodTypeID);
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItem);
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
