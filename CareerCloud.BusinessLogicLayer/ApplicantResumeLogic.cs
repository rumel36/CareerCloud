using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic: BaseLogic<ApplicantResumePoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository) {}

        protected override void Verify(ApplicantResumePoco[] pocos)
        {
            foreach(var poco in pocos) {
                if (string.IsNullOrEmpty(poco.Resume)) {
                    exceptions.Add(new ValidationException(113, $"ApplicationResume field {poco.Resume} can not be empty"));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
