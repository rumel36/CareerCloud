using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository):base(repository){}
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (string.IsNullOrEmpty(poco.CompanyDescription)) {

                    exceptions.Add(new ValidationException(107, $"Company Description {poco.CompanyDescription} must be greater than 2 characters"));
                }
                else if (poco.CompanyDescription.Length <= 2) {
                    exceptions.Add(new ValidationException(107, $"Company Description {poco.CompanyDescription} must be greater than 2 characters"));
                }
                if (string.IsNullOrEmpty(poco.CompanyName))
                { 
                    exceptions.Add(new ValidationException(106, $"Company Name {poco.CompanyName} must be greater than 2 characters")); 
                }
                else if (poco.CompanyName.Length <=2) {
                    exceptions.Add(new ValidationException(106, $"Company Name {poco.CompanyName} must be greater than 2 characters"));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
