﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco:IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("FK_Company_Jobs_Company_Profiles")]
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }

        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }

        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducation { get; set; }

        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkill { get; set; }

        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
            
       }
}
