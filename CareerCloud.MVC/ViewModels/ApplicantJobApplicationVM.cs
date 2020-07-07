using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class ApplicantJobApplicationVM
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public DateTime ApplicationDate { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
    }
}
