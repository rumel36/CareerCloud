﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class JobPostedVM
    {
        [Key]
        public Guid JobId { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Job Posted")]
        public DateTime JobCreated { get; set; }
    }
}
