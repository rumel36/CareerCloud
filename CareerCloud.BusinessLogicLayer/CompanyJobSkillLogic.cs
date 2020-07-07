using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic: BaseLogic<CompanyJobSkillPoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository) { }

        protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            foreach (var poco in pocos) {
                if (poco.Importance < 0) {
                    exceptions.Add(new ValidationException(400, $"The field {poco.Importance} can not be less than 0 "));
                }
            }
            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions);
            }
        }
        public override void Update(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }


    }
}
