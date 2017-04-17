﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using zaard_application.Models;


namespace zaard_application.Controllers {


    public class BuyItemsController : Controller {
        //private zaardCurrentEntities db = new zaardCurrentEntities();
        private zaardNetworkEntities db = new zaardNetworkEntities();
        string userEmail = "";
        
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

        public ActionResult moreInfo(int buyItemId)
        {
            BuyItem selectedItem = (from b in db.BuyItems where b.buyItemID == buyItemId select b).FirstOrDefault();
            return View(selectedItem);
           
        }

        public ActionResult Review(int buyItemId)
        {
            List<Review> Review = (from b in db.Reviews where b.buyItemID == buyItemId select b).ToList();
            //BuyItem selectedItem = (from b in db.BuyItems where b.buyItemID == buyItemId select b).FirstOrDefault();
            return View(Review);

        }

        //public ActionResult Review1(int buyItemId)
        //{
        //    return RedirectToAction("moreInfo", buyItemId);

        //}

        public ActionResult addAddressPage()
        {
            return View();
        }

        public ActionResult AddAddress(address address, string useremail)
        {

           address newAddress = new address();
           int userIdFromAddress = (from u in db.Users where u.email == useremail select u.userID).FirstOrDefault();
            //string emailFromAddress = (from u in db.Users where u.email == useremail select u.email).FirstOrDefault();

            newAddress.street = address.street;
            newAddress.city = address.city;
            newAddress.state = address.state;
            newAddress.zipcode = address.zipcode;
            newAddress.UserID = userIdFromAddress;
            newAddress.email = useremail;

            Random rand = new Random();
            newAddress.addressID = rand.Next();
           // address.UserID = userIdFromAddress;
           // address.email = emailFromAddress;
            db.addresses.Add(newAddress);
            db.SaveChanges();
            return RedirectToAction("AddPaymentPage");
        }

        public ActionResult AddPaymentPage()
        {
            return View();
        }

        public ActionResult AddPayment(paymentInfo payment, string useremail)
        {
            paymentInfo newPayment = new paymentInfo();
            int userIdFromPayment = (from u in db.Users where u.email == useremail select u.userID).FirstOrDefault();
            newPayment.cardNum = payment.cardNum;
            newPayment.cardType = payment.cardType;
            newPayment.cvv = payment.cvv;
            newPayment.expiration = payment.expiration;
            newPayment.timeOfPurchase = DateTime.Now;
            newPayment.nameOnCard = payment.nameOnCard;
            newPayment.userID = userIdFromPayment;

            Random rand = new Random();
            newPayment.paymentID = rand.Next();

            db.paymentInfoes.Add(newPayment);
            db.SaveChanges();
            return RedirectToAction("UserDetailsConfirmation", new { email = useremail });
        }


        //public class addressAndPaymentInfo
        //{
        //    public address AddressDetails { get; set; }
        //    public paymentInfo PaymentInfoDetails { get; set; }
        //}

        public ActionResult UserDetailsConfirmation(string email)
        {
            userEmail = email;
            int userId = (from u in db.Users where u.email == email select u.userID).FirstOrDefault();
            var userAddress = (from a in db.addresses where a.UserID == userId select a).FirstOrDefault();
            var userPaymentInfo = (from p in db.paymentInfoes where p.userID == userId select p).FirstOrDefault();

            var model = new addressAndPaymentViewModel { AddressDetails = userAddress, PaymentInfoDetails = userPaymentInfo };
            return View(model);
        }

        public ActionResult PaymentConfirmedPage()
        {
            User selectedUser = (from b in db.Users where b.email == userEmail select b).FirstOrDefault();

            const string accountName = "gupta.shambhavi27@gmail.com";            // # Gmail account name
            const string password = "Ganeshji27";                                // # Gmail account password
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            smtp.Credentials = new System.Net.NetworkCredential(accountName, password);

            mail.From = new MailAddress("gupta.shambhavi27@gmail.com"); // # Remember to change here with the mail you got
            mail.To.Add("shambhavi123@yahoo.co.in");                                  // # Email adress to send activation mail
            mail.Subject = "Thanks for shopping!";
            mail.Body = "Here are the details related to your purchase!";   // # You will need to change here with HTML containing a link (which contains a generated activation code)
            //mail.IsHtml = true;
            
            smtp.Send(mail);

            return View(selectedUser);
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
