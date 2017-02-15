using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fortune_Teller_MVC.Models;

namespace Fortune_Teller_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //RETURN FORTUNE
            //Retirement Age
            if (customer.Age % 2 == 0)
            {
                ViewBag.RetirementAge = " 75 ";
            }
            else
            {
                ViewBag.RetirementAge = " 65 ";
            }

            //Retirement Money
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.RetirementMoney = " $500,000.00 ";
            }
            else if (customer.BirthMonth > 4 && customer.BirthMonth <= 8)
            {
                ViewBag.RetirementMoney = " $900,000.00 ";
            }
            else if (customer.BirthMonth > 8 && customer.BirthMonth <= 12)
            {
                ViewBag.RetirementMoney = " $50,000.00 ";
            }

            //Mode of Transportation
            switch (customer.FavoriteColor)
            {
                case "Red":
                    ViewBag.ModeOfTransportation = " Lambo";
                    break;
                case "Orange":
                    ViewBag.ModeOfTransportation = " Toyota Camry";
                    break;
                case "Yellow":
                    ViewBag.ModeOfTransportation = " Bentley";
                    break;
                case "Green":
                    ViewBag.ModeOfTransportation = " Subaru";
                    break;
                case "Blue":
                    ViewBag.ModeOfTransportation = " Ducati";
                    break;
                case "Indigo":
                    ViewBag.ModeOfTransportation = " Tesla";
                    break;
                case "Violet":
                    ViewBag.ModeOfTransportation = " Volkswagen";
                    break;
            }

            //Vacation Home
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.VacationHome = " Paris";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.VacationHome = " Barcelona";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.VacationHome = " Venice";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.VacationHome = " Madrid";
            }
            else if (customer.NumberOfSiblings > 3)
            {
                ViewBag.VacationHome = " Miami";
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
