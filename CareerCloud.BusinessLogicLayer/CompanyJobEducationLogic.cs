using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository) {
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(200, $"{poco.Major} at least 2 characters or greater"));
                }
                else if (poco.Major.Length <2) {
                    exceptions.Add(new ValidationException(200, $"{poco.Major} at least 2 characters or greater"));
                }
                if (poco.Importance < 0) {
                    exceptions.Add(new ValidationException(201, $"{poco.Importance} should not be less than zero"));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
