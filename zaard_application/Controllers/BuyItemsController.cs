using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using zaard_application.Models;

namespace zaard_application.Controllers
{
    public class BuyItemsController : Controller
    {
        private zaardCurrentEntities db = new zaardCurrentEntities();

        // GET: BuyItems
        public ActionResult Index()
        {
            var buyItems = db.BuyItems.Include(b => b.Supplier);
            return View(buyItems.ToList());
        }

        public ActionResult BookDetails()
        {
            string itemTYPE = "Book";
            List<BuyItem> Book = (from b in db.BuyItems where b.itemType == itemTYPE select b).ToList() ;
            return View(Book);
        }

        public ActionResult GameDetails()
        {
            string itemTYPE = "Game";
            List<BuyItem> Game = (from b in db.BuyItems where b.itemType == itemTYPE select b).ToList();
            return View(Game);
        }

        public ActionResult MovieDetails()
        {
            string itemTYPE = "Movie";
            List<BuyItem> Movie = (from b in db.BuyItems where b.itemType == itemTYPE select b).ToList();
            return View(Movie);
        }

        // GET: BuyItems/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BuyItem buyItem = db.BuyItems.Find(id);
        //    if (buyItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(buyItem);
        //}

        // GET: BuyItems/Create
        public ActionResult Create()
        {
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierPhone");
            return View();
        }

        // POST: BuyItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "buyItemID,stock,price,released,itemType,genre,fiction,minAge,itemName,itemLink,itemLocation,itemDescription,supplierID,isDigital,image")] BuyItem buyItem)
        {
            if (ModelState.IsValid)
            {
                db.BuyItems.Add(buyItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierPhone", buyItem.supplierID);
            return View(buyItem);
        }

        // GET: BuyItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyItem buyItem = db.BuyItems.Find(id);
            if (buyItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierPhone", buyItem.supplierID);
            return View(buyItem);
        }

        // POST: BuyItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "buyItemID,stock,price,released,itemType,genre,fiction,minAge,itemName,itemLink,itemLocation,itemDescription,supplierID,isDigital,image")] BuyItem buyItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierPhone", buyItem.supplierID);
            return View(buyItem);
        }

        // GET: BuyItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyItem buyItem = db.BuyItems.Find(id);
            if (buyItem == null)
            {
                return HttpNotFound();
            }
            return View(buyItem);
        }

        // POST: BuyItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuyItem buyItem = db.BuyItems.Find(id);
            db.BuyItems.Remove(buyItem);
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
