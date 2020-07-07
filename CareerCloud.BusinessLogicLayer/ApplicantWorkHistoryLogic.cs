using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository): base(repository) {}
        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (string.IsNullOrEmpty(poco.CompanyName)) {
                    exceptions.Add(new ValidationException(105, $"CompanyName {poco.CompanyName} Must be greater than 2 characters"));
                }
                else if (poco.CompanyName.Length <= 2)
                {
                    exceptions.Add(new ValidationException(105, $"CompanyName {poco.CompanyName} Must be greater than 2 characters"));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
