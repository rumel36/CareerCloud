using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class CompanyJobVM
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsInactive { get; set; }
        public bool IsInternalPosition { get; set; }
    }
}
