using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {

        List<ValidationException> exceptions = new List<ValidationException>();
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco>
                 repository) : base(repository) { }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, $"The field {poco.Major} cannot be null or not less than 3 characters"));
                }
                else if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"The field {poco.Major} cannot be null or not less than 3 characters"));
                }
                if (poco.StartDate > DateTime.Now) {
                    exceptions.Add(new ValidationException(108, $"Start date {poco.StartDate} can not be greater than today"));
                }
                if (poco.CompletionDate < poco.StartDate) {
                    exceptions.Add(new ValidationException(109,  $"That is weird"));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
            
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }

}
