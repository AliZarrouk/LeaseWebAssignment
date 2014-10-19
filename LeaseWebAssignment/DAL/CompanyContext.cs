using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LeaseWebAssignment.Models;
using System.Data.Entity.Validation;

namespace LeaseWebAssignment.DAL
{
    public class CompanyContext : DbContext
    {
        public CompanyContext()
            : base("CompanyContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            DbEntityValidationResult Errors = new DbEntityValidationResult(entityEntry,new List<DbValidationError>());
            DbValidationError dve;
            object obj = entityEntry.Entity;
            Customer cus = new Customer();
            bool test = obj.GetType().IsEquivalentTo(cus.GetType());
            if (test)
            {
                Customer customer = obj as Customer;
                if (customer.registrationNbr.ToString().Length != 10)
                {
                    dve = new DbValidationError(
    "registrationNbr",
    "Registration number must be 10 digits");
                    Errors.ValidationErrors.Add(dve);
                }
            }
            if (Errors.ValidationErrors.Count > 0)
            {
                return Errors;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
        }
    }
}