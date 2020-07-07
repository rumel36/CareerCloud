using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class CreateJobVM
    { 
     
            public Guid CompanyId { get; set; }
            public Guid JobId { get; set; }
            public string JobTitle { get; set; }
            public string JobDescription { get; set; }
            public DateTime ProfileCreated { get; set; }
      
            public bool IsInactive { get; set; }
            public bool IsCompanyHidden { get; set; }
        
    }
}
