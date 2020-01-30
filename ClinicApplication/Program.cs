using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicApp.Models;
using ClinicApplication.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClinicApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=app.db");

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                RemoveData(context);

                var doc = new Doctor()
                {
                    FirstName = "Bill",
                    LastName = "Dolittle",
                    Telephone = "576-723-984",
                    MedicalServices = new[] {new MedicalService()
                    {
                        Description = "Usuwanie brodawek",
                        Price = 100
                    }},
                    Offices = new[] {new Office()
                    {
                        City = "Warszawa",
                        Number = "7/21",
                        PostCode = "70-190",
                        Street = "Agrarna",
                        Telephone = "564-086-247"
                    }}
                };
                
                var doc2 = new Doctor()
                {
                    FirstName = "Artur",
                    LastName = "King",
                    Telephone = "873-435-687",
                    MedicalServices = new[] {
                        new MedicalService()
                        {
                            Description = "Wizyta rodzinna",
                            Price = 150
                        },
                        new MedicalService()
                        {
                            Description = "Badanie moczu",
                            Price = 30
                        }
                    },
                    Offices = new[] {new Office()
                    {
                        City = "Gdańsk",
                        Number = "13",
                        PostCode = "80-170",
                        Street = "Bethovena",
                        Telephone = "533-937-083"
                    }}
                };
                
                context.Doctors.AddRange(doc, doc2);
                context.SaveChanges();
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static void RemoveData(ApplicationDbContext context)
        {
            //removing
            List<Doctor> doctors = context.Doctors.ToList();
            context.Doctors.RemoveRange(doctors);
                
            List<Office> offices = context.Offices.ToList();
            context.Offices.RemoveRange(offices);

            List<MedicalService> medicalServices = context.MedicalServices.ToList();
            context.MedicalServices.RemoveRange(medicalServices);

            context.SaveChanges();   
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}