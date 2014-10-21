using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace LeaseWebAssignment.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long registrationNbr { get; set; }
        public string companyName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public virtual City city { get; set; }
        public virtual Country country { get; set; }
        public string phoneNumber { get; set; }
        public string website { get; set; }
        public uint numberOfEmployees { get; set; }
        public uint maximumOutstandingOrderAmount { get; set; }
        public DateTime customerSince { get; set; }
        public virtual ICollection<Contact> contacts { get; set; }

        public bool IsValid(DbEntityValidationResult errors)
        {
            ValidateRegistrationNumberDigitsNumber(errors);

            ValidateNumberOfEmployeesMoreThanOne(errors);

            ValidateContactTypes(errors);

            ValidateUnicityOfEmailsWithinSameCustomer(errors);

            ValidateEmailAddress(errors);

            if (errors.ValidationErrors.Count > 0)
                return false;
            return true;
        }

        private void ValidateEmailAddress(DbEntityValidationResult errors)
        {
            try
            {
                if (!Utilities.Utilities.IsValidEmail(email))
                {
                    DbValidationError dve;
                    dve = new DbValidationError("email", "Email address is not valid");
                    errors.ValidationErrors.Add(dve);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValidateUnicityOfEmailsWithinSameCustomer(DbEntityValidationResult errors)
        {
            try
            {
                List<string> emails = new List<string>();
                foreach (Contact contact in contacts)
                {
                    if (emails.Contains(contact.email))
                    {
                        DbValidationError dve;
                        dve = new DbValidationError("contacts", "E-mails of the contacts within the same customer must be unique");
                        errors.ValidationErrors.Add(dve);
                        break;
                    }
                    emails.Add(contact.email);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValidateContactTypes(DbEntityValidationResult errors)
        {
            try
            {
                bool foundMainContact = false;
                bool foundFinancialContact = false;
                bool foundTechnicalContact = false;

                foreach (Contact contact in contacts)
                {
                    if (contact.type.Contains(ContactType.Financial))
                        foundFinancialContact = true;

                    if (contact.type.Contains(ContactType.Main))
                        foundMainContact = true;

                    if (contact.type.Contains(ContactType.Technical))
                        foundTechnicalContact = true;
                }

                if (!(foundMainContact && foundFinancialContact && foundTechnicalContact))
                {
                    DbValidationError dve;
                    dve = new DbValidationError("contacts", "Customers must have only one Main, Financial and Technical contact");
                    errors.ValidationErrors.Add(dve);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValidateNumberOfEmployeesMoreThanOne(DbEntityValidationResult errors)
        {
            try
            {
                if (numberOfEmployees < 1)
                {
                    DbValidationError dve;
                    dve = new DbValidationError("numberOfEmployees", "Number of employees must be at least 1");
                    errors.ValidationErrors.Add(dve);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValidateRegistrationNumberDigitsNumber(DbEntityValidationResult errors)
        {
            try
            {
                if (this.registrationNbr.ToString().Length != 10)
                {
                    DbValidationError dve;
                    dve = new DbValidationError("registrationNbr", "Registration number must be 10 digits");
                    errors.ValidationErrors.Add(dve);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

    }
}