using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobLogic : BaseLogic<CompanyJobPoco>
    {
        List<ValidationException> exceptions = new List<ValidationException>();
        public CompanyJobLogic(IDataRepository<CompanyJobPoco> repository) : base(repository){}

      
    }
}
