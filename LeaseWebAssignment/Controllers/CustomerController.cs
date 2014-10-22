using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaseWebAssignment.DAL;
using LeaseWebAssignment.Models;
using System.Data.Entity.Validation;

namespace LeaseWebAssignment.Controllers
{
    public class CustomerController : Controller
    {
        private CompanyContext db = new CompanyContext();

        // GET: Customer
        /*
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        */
        // GET: Customer/Details/5
        public ActionResult Details(long? id)
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

        // GET: Customer/Create
        public ActionResult Create()
        {
            try {
                List<Country> countries = db.Countries.OrderBy(q => q.name).ToList();
                ViewBag.CountriesNameList = new SelectList(countries, "iso", "name");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "registrationNbr,companyName,phoneNumber,website,customerSince")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(customer);
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View();
            }

        }

        public ActionResult Index(string sortOrder, string regNbrSearchString, string emailSearchString, int? SelectedCountry)
        {
            try
            {
                List<Country> countries = db.Countries.OrderBy(q => q.name).ToList();
                ViewBag.SelectedCountry = new SelectList(countries, "iso", "name", SelectedCountry);


                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                var customers = from s in db.Customers
                                select s;

                if (!String.IsNullOrEmpty(regNbrSearchString))
                {
                    customers = customers.Where(s => s.registrationNbr.ToString().ToUpper().Contains(regNbrSearchString.ToUpper()));
                }

                if (!String.IsNullOrEmpty(emailSearchString))
                {
                    customers = customers.Where(s => s.email.ToString().ToUpper().Contains(emailSearchString.ToUpper()));
                }

                switch (sortOrder)
                {
                    case "Date":
                        customers = customers.OrderBy(s => s.customerSince);
                        break;
                    case "date_desc":
                        customers = customers.OrderByDescending(s => s.customerSince);
                        break;
                    default:
                        customers = customers.OrderBy(s => s.companyName);
                        break;
                }

                bool hasValue = SelectedCountry.HasValue;

                if (hasValue)
                {
                    int countryID = SelectedCountry.GetValueOrDefault();

                    var selectedCustomers = db.Customers.Where(cus => cus.country.name == countries[countryID].name);
                    return View(selectedCustomers.ToList());
                }
                return View(customers.ToList());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return View(db.Customers.ToList());
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(long? id)
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

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "registrationNbr,companyName,phoneNumber,website,customerSince")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(long? id)
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

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
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
