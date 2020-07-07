using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class ApplicantProfileVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ApplicantProfilePoco ApplicantProfile { get; set; }
           
    }
}
