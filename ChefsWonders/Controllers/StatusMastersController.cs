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
    public class StatusMastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StatusMasters
        public ActionResult Index()
        {
            return View(db.StatusMasters.ToList());
        }

        // GET: StatusMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // GET: StatusMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statuscode,status")] StatusMaster statusMaster)
        {
            if (ModelState.IsValid)
            {
                db.StatusMasters.Add(statusMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusMaster);
        }

        // GET: StatusMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // POST: StatusMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statuscode,status")] StatusMaster statusMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusMaster);
        }

        // GET: StatusMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // POST: StatusMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            db.StatusMasters.Remove(statusMaster);
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
