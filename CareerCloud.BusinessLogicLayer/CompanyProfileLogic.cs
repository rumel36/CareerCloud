using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository):base(repository) { }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(601, $" {poco.CompanyWebsite} is required"));
                }
                else if(!Regex.IsMatch(poco.CompanyWebsite, @".*([^\.]+)(ca|com|biz)$", RegexOptions.IgnoreCase))
                {
                    exceptions.Add(new ValidationException(600, $"The field {poco.CompanyWebsite} does not have valid website address extention"));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $" {poco.ContactPhone} is required"));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber for Company Profile {poco.ContactPhone} is not in the required format."));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for Company Profile {poco.ContactPhone} is not in the required format."));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for Company Profile {poco.ContactPhone} is not in the required format."));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber for Company Profile {poco.ContactPhone} is not in the required format."));
                        }
                    }
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
           // Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
         //   Verify(pocos);
            base.Add(pocos);
        }

    }
}
