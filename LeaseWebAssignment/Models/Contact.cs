using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string name { get; set; }
        public Title title { get; set; }
        public virtual ICollection<ContactType> type { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public virtual Country country { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public virtual Contact parentContact { get; set; }

        public bool IsValid(DbEntityValidationResult errors)
        {
            ValidateEmailAddress(errors);

            ValidateParentContact(errors);

            ValitdatePhoneNumber(errors);

            return true;
        }

        private void ValidateParentContact(DbEntityValidationResult errors)
        {
            try
            {
                if ((! type.Contains(ContactType.Main)) && (this.parentContact != null))
                    {
                        DbValidationError dve;
                        dve = new DbValidationError("type", "Every contact must have a parent contact (except the Main one)");
                        errors.ValidationErrors.Add(dve);
                    }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValitdatePhoneNumber(DbEntityValidationResult errors)
        {
            try
            {
                if (!Utilities.Utilities.isValidPhoneNumber(phoneNumber))
                {
                    DbValidationError dve;
                    dve = new DbValidationError("phoneNumber", "Phone number is not valid");
                    errors.ValidationErrors.Add(dve);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
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
    }
}
