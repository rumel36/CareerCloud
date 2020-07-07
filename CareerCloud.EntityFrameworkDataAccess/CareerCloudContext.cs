using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext: DbContext 
    {
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options)
           : base(options)
        {

        }
        public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRole { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(entity => {
                entity.HasOne(e=>e.ApplicantProfile).WithMany(p=>p.ApplicantEducation).HasForeignKey(e=>e.Applicant);
            });
            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity => {
                entity.HasOne(e => e.ApplicantProfile).WithMany(p => p.ApplicantJobApplication).HasForeignKey(e => e.Applicant);
                entity.HasOne(e => e.CompanyJob).WithMany(p => p.ApplicantJobApplication).HasForeignKey(e => e.Job);
            });  
            modelBuilder.Entity<ApplicantProfilePoco>(entity => {
                entity.HasOne(e => e.SystemCountryCode).WithMany(p => p.ApplicantProfile).HasForeignKey(e => e.Country);
                entity.HasOne(e => e.SecurityLogin).WithMany(p => p.ApplicantProfile).HasForeignKey(e => e.Login);
            });
            modelBuilder.Entity<ApplicantResumePoco>(entity => {
                entity.HasOne(e => e.ApplicantProfile).WithMany(p => p.ApplicantResume).HasForeignKey(e => e.Applicant);
            });

            modelBuilder.Entity<ApplicantSkillPoco>(entity => {
                entity.HasOne(e => e.ApplicantProfile).WithMany(p => p.ApplicantSkill).HasForeignKey(e => e.Applicant);
            });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity => {
                entity.HasOne(e => e.ApplicantProfile).WithMany(p => p.ApplicantWorkHistory).HasForeignKey(e => e.Applicant);
                entity.HasOne(e => e.SystemCountryCode).WithMany(p => p.ApplicantWorkHistory).HasForeignKey(e => e.CountryCode);
            });

            modelBuilder.Entity<CompanyDescriptionPoco>(entity => {
                entity.HasOne(e => e.CompanyProfile).WithMany(p => p.CompanyDescription).HasForeignKey(e => e.Company);
                entity.HasOne(e => e.SystemLanguageCode).WithMany(p => p.CompanyDescription).HasForeignKey(e => e.LanguageId);
            });

            modelBuilder.Entity<CompanyJobDescriptionPoco>(entity => {
                entity.HasOne(e => e.CompanyJob).WithMany(p => p.CompanyJobDescription).HasForeignKey(e => e.Job);
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(entity => {
                entity.HasOne(e => e.CompanyJob).WithMany(p => p.CompanyJobEducation).HasForeignKey(e => e.Job);
            });

            modelBuilder.Entity<CompanyJobPoco>(entity => {
                entity.HasOne(e => e.CompanyProfile).WithMany(p => p.CompanyJob).HasForeignKey(e => e.Company);
            });

            modelBuilder.Entity<CompanyJobSkillPoco>(entity => {
                entity.HasOne(e => e.CompanyJob).WithMany(p => p.CompanyJobSkill).HasForeignKey(e => e.Job);
            });

            modelBuilder.Entity<CompanyLocationPoco>(entity => {
                entity.HasOne(e => e.CompanyProfile).WithMany(p => p.CompanyLocation).HasForeignKey(e => e.Company);
            });

            modelBuilder.Entity<SecurityLoginsLogPoco>(entity => {
                entity.HasOne(e => e.SecurityLogin).WithMany(p => p.SecurityLoginsLog).HasForeignKey(e => e.Login);
            });

            modelBuilder.Entity<SecurityLoginsRolePoco>(entity => {
                entity.HasOne(e => e.SecurityRole).WithMany(p => p.SecurityLoginsRole).HasForeignKey(e => e.Role);
                entity.HasOne(e => e.SecurityLogin).WithMany(p => p.SecurityLoginsRole).HasForeignKey(e => e.Login);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
