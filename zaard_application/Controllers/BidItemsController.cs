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
    public class BidItemsController : Controller
    {
        //private zaardNetworkEntities db = new zaardNetworkEntities();
        private zaardCurrentEntities db = new zaardCurrentEntities();

        // GET: BidItems
        //public ActionResult Index()
        //{
        //    var bidItems = db.BidItems.Include(b => b.Bid).Include(b => b.User);
        //    return View(bidItems.ToList());
        //}

        public ActionResult BookDetails()
        {
            string itemTYPE = "Book";
            List<BidItem> Book = (from b in db.BidItems where b.itemCategory == itemTYPE select b).ToList();
            if (Book == null)
            {
                RedirectToAction("Index", "Home");
            }
            return View(Book);
        }

        public ActionResult GameDetails()
        {
            string itemTYPE = "Game";
            List<BidItem> Game = (from b in db.BidItems where b.itemCategory == itemTYPE select b).ToList();
            return View(Game);
        }

        public ActionResult MovieDetails()
        {
            string itemTYPE = "Movie";
            List<BidItem> Movie = (from b in db.BidItems where b.itemCategory == itemTYPE select b).ToList();
            return View(Movie);
        }
        public ActionResult moreInfo(int bidItemId)
        {
            int maxBid;
            int bidCount = (from b in db.Bids where b.bidItemID == bidItemId select b.bidAmount).Count();
            if (bidCount > 0)
            {
                maxBid = (from b in db.Bids where b.bidItemID == bidItemId select b.bidAmount).Max();
            }
            else
            {
                maxBid = 0;
            }
            if (maxBid == 0)
            {
                Session["maxBid"] = "No Bid has been made yet";
            }
            else
            {
                Session["maxBid"] = maxBid;
            }
            Session["bidItemId"] = bidItemId;
            BidItem selectedItem = (from b in db.BidItems where b.bidItemID == bidItemId select b).FirstOrDefault();
            Session["bidItemName"] = selectedItem.itemName;
            return View(selectedItem);

        }

        public ActionResult makeBidPage ()
        {

            return View();
        }

        public ActionResult submitBid (Bid bid, int bidItemID, string userEmail)
        {
            Bid newBid = new Models.Bid();
            newBid.bidAmount = bid.bidAmount;
            newBid.bidTime = DateTime.Now;
            User bidingUser = (from u in db.Users where u.email == userEmail select u).FirstOrDefault();
            newBid.userID = bidingUser.userID;
            newBid.bidItemID = bidItemID;
            Random rand = new Random();
            newBid.bidID = rand.Next();
            BidItem bidItem = (from u in db.BidItems where u.bidItemID == bidItemID select u).FirstOrDefault();

            int maxBid;
            int bidCount = (from b in db.Bids where b.bidItemID == bidItemID select b.bidAmount).Count();
            if (bidCount > 0)
            {
                maxBid = (from b in db.Bids where b.bidItemID == bidItemID select b.bidAmount).Max();
            }
            else
            {
                maxBid = 0;
            }

            int currentMax = maxBid;


            if (bid.bidAmount < currentMax || newBid.bidTime > bidItem.auctionEnd)
            {
                return RedirectToAction("bidErrorPage");
            }
            else
            {
                db.Bids.Add(newBid);
                db.SaveChanges();
                return RedirectToAction("moreinfo", new { bidItemId = bidItemID });
            }
            return null;
        }

        public ActionResult bidErrorPage ()
        {
            return View();
        }

        // GET: BidItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BidItem bidItem = db.BidItems.Find(id);
            if (bidItem == null)
            {
                return HttpNotFound();
            }
            return View(bidItem);
        }

        // GET: BidItems/Create
        public ActionResult Create()
        {
            ViewBag.bidItemID = new SelectList(db.Bids, "bidItemID", "bidItemID");
            ViewBag.UserID = new SelectList(db.Users, "userID", "password");
            return View();
        }

        // POST: BidItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itemCategory,itemName,itemLink,itemLocation,itemDescription,UserID,isDigital,bidItemID,auctionStatus,auctionStart,auctionEnd,image")] BidItem bidItem)
        {
            if (ModelState.IsValid)
            {
                db.BidItems.Add(bidItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bidItemID = new SelectList(db.Bids, "bidItemID", "bidItemID", bidItem.bidItemID);
            //ViewBag.UserID = new SelectList(db.Users, "userID", "password", bidItem.UserID);
            return View(bidItem);
        }

        // GET: BidItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BidItem bidItem = db.BidItems.Find(id);
            if (bidItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.bidItemID = new SelectList(db.Bids, "bidItemID", "bidItemID", bidItem.bidItemID);
            //ViewBag.UserID = new SelectList(db.Users, "userID", "password", bidItem.UserID);
            return View(bidItem);
        }

        // POST: BidItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itemCategory,itemName,itemLink,itemLocation,itemDescription,UserID,isDigital,bidItemID,auctionStatus,auctionStart,auctionEnd,image")] BidItem bidItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bidItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bidItemID = new SelectList(db.Bids, "bidItemID", "bidItemID", bidItem.bidItemID);
           // ViewBag.UserID = new SelectList(db.Users, "userID", "password", bidItem.UserID);
            return View(bidItem);
        }

        // GET: BidItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BidItem bidItem = db.BidItems.Find(id);
            if (bidItem == null)
            {
                return HttpNotFound();
            }
            return View(bidItem);
        }

        // POST: BidItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BidItem bidItem = db.BidItems.Find(id);
            db.BidItems.Remove(bidItem);
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
