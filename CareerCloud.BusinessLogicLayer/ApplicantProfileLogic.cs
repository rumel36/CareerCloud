using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco>
                 repository) : base(repository) {}

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (poco.CurrentSalary < 0) {
                    exceptions.Add(new ValidationException(111, $"Current Salary {poco.CurrentSalary} can not be negative"));
                }
                if (poco.CurrentRate < 0) {
                    exceptions.Add(new ValidationException(112, $"Current Rate {poco.CurrentRate} can not be negative"));
                }
            }
            if (exceptions.Count > 0) {

                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}
