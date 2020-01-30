using System;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Models;
using ClinicApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ClinicApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        public DoctorController(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        
        [Authorize]
        [Route("Doctor/Add")]
        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }
        
        [Authorize]
        [Route("Doctor/Add")]
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctorObj)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new ApplicationDbContext(_contextOptions))
                {
                    var doctor = new Doctor()
                    {
                        FirstName = doctorObj.FirstName,
                        LastName = doctorObj.LastName,
                        Telephone = doctorObj.Telephone
                    };

                    dbContext.Doctors.Add(doctor);
                    dbContext.SaveChanges();
                }
                return Content("You added doctor!");

            }

            return View();
        }
        
        [Authorize(Roles = "Admin")]
        [Route("Doctor")]
        [HttpPost]
        public IActionResult OnDelete(int id)
        {
            using (var dbContext = new ApplicationDbContext(_contextOptions))
            {
                Doctor d = dbContext.Doctors
                    .Include(b => b.MedicalServices)
                    .Include(b => b.Offices)
                    .First(d => d.Id == id);

                foreach (var child in d.MedicalServices.ToList())
                    dbContext.MedicalServices.Remove(child);
                
                foreach (var child in d.Offices.ToList())
                    dbContext.Offices.Remove(child);
                
                dbContext.Doctors.Remove(d);
                
                dbContext.SaveChanges();
            }
            
            return Content("You removed doctor!");
        }

        
        [Route("Doctors")]
        public IActionResult Doctors()
        {
            List<Doctor> dbContextDoctors;
            
            using (var dbContext = new ApplicationDbContext(_contextOptions))
            {
                dbContextDoctors = dbContext.Doctors.ToList();
            }

            return View(dbContextDoctors);
        }

        [Route("Doctor")]
        public IActionResult DoctorsById(int id)
        {
            List<Doctor> doctors;

            using (var ctx = new ApplicationDbContext(_contextOptions))
            {
                doctors = ctx.Doctors
                    .Include(d => d.MedicalServices)
                    .Include(d => d.Offices)
                    .Where(d => d.Id==id)
                    .ToList();
            }

            ViewData["Doctors"] = doctors;

            if (doctors != null)
                return View();
            
            return NotFound();
        }
    }
}