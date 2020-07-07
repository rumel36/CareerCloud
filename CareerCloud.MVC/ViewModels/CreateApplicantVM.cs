using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.MVC.ViewModels
{
    public class CreateApplicantVM
    {

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
    }
}
