﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaseWebAssignment.DAL;
using LeaseWebAssignment.Models;

namespace LeaseWebAssignment.Controllers
{
    public class WorkContactController : Controller
    {
        private CompanyContext db = new CompanyContext();

        public ActionResult CreateWorkContactProfile()
        {
            var workContactProfileViewModel = new Contact();
            //{ type = new ContactType() { value = ContactTypeValue.Main, assigned = false } };

            return View(workContactProfileViewModel);
        }

        // GET: WorkContact
        public ActionResult Index(string nameSearchString, string emailSearchString, int? SelectedCountry)
        {
            try
            {
                List<Country> countries = db.Countries.OrderBy(q => q.name).ToList();
                ViewBag.SelectedCountry = new SelectList(countries, "iso", "name", SelectedCountry);

                var contacts = from s in db.Contacts
                                select s;

                if (!String.IsNullOrEmpty(nameSearchString))
                {
                    contacts = contacts.Where(s => s.name.ToString().ToUpper().Contains(nameSearchString.ToUpper()));
                }

                if (!String.IsNullOrEmpty(emailSearchString))
                {
                    contacts = contacts.Where(s => s.email.ToString().ToUpper().Contains(emailSearchString.ToUpper()));
                }

                bool hasValue = SelectedCountry.HasValue;

                if (hasValue)
                {
                    int countryID = SelectedCountry.GetValueOrDefault();

                    var selectedCustomers = db.Customers.Where(cus => cus.country.name == countries[countryID].name);
                    return View(selectedCustomers.ToList());
                }

                return View(contacts.ToList());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return View(db.Contacts.ToList());
            }
        }

        // GET: WorkContact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: WorkContact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkContact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,title,phoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: WorkContact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: WorkContact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,title,phoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: WorkContact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: WorkContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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
