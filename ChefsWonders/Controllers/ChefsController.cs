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
    public class ChefsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chefs
        public ActionResult Index()
        {
            var chefs = db.Chefs.Include(c => c.statusMaster);
            return View(chefs.ToList());
        }

        // GET: Chefs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        // GET: Chefs/Create
        public ActionResult Create()
        {
            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status");
            return View();
        }

        // POST: Chefs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChefID,ChefName,statuscode")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                db.Chefs.Add(chef);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", chef.statuscode);
            return View(chef);
        }

        // GET: Chefs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", chef.statuscode);
            return View(chef);
        }

        // POST: Chefs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChefID,ChefName,statuscode")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chef).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statuscode = new SelectList(db.StatusMasters, "statuscode", "status", chef.statuscode);
            return View(chef);
        }

        // GET: Chefs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chef chef = db.Chefs.Find(id);
            db.Chefs.Remove(chef);
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
