﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PFE_Management.Areas.Identity.Pages.Account;
using PFE_Management.Data;
using PFE_Management.Models;

namespace PFE_Management.Controllers
{
  
   
    public class StudentsController : Controller
    {
        //Injection des dependances 
        private readonly ApplicationDbContext _context;


        private readonly UserManager <IdentityUser> _userManager;
        private SignInManager <IdentityUser> _signManager;

        public StudentsController(  ApplicationDbContext context , UserManager <IdentityUser> user , SignInManager<IdentityUser> signManager)
                                   
        {
            _context = context;
            _userManager = user;
            _signManager = signManager ; 

        }

        // GET: Students
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 4;
            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                // ici on ajouter une instance d'un student
                .Include(s => s.Enrollments)
                   .ThenInclude(s => s.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }



        // GET: Students/Create

        // [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

       

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,Email,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost , ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,"",s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }

        /*    this for Registration    */

        //Get for Register
        public async Task<IActionResult> Register(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RegisterModel = await _context.Students.FindAsync(id);
            if (RegisterModel == null)
            {
                return NotFound();
            }
            return View(RegisterModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student model , int? id)
        {

            
            if (ModelState.IsValid )
            {
                IdentityUser user = new IdentityUser() {  UserName=model.Email , 
                                                          Email = model.Email , 
                                                          EmailConfirmed = true};

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //Ici Pour Editer le champs Has Account to yes une fois l'Admin creer un compte pour cet Utilisateur
                    var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                    if (await TryUpdateModelAsync<Student>(
                        studentToUpdate, "", s => s.etatAccount))
                    {
                        try
                        {
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        catch (DbUpdateException /* ex */)
                        {
                            //Log the error (uncomment ex variable name and write a log.)
                            ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists, " +
                                "see your system administrator.");
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Students");
        }

        // get stage view with all instructors 
        //public IActionResult CreateStage()
        //{
        //    //PopulateInstructorsDropDownList();
        //    PopulateDepartmentsDropDownList();
        //    ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstMidName");
        //    // var studentid = await _context.Students.FindAsync(id);
        //    ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    //ViewData["StudentIDhashed"] = userId;

        //    return View();
        //}



        // create stage version 2 with iduser of Instructor
        public async Task<IActionResult> CreateStage()
        {
            //PopulateInstructorsDropDownList();


            //-----------------------------------
            var idStudentStage = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stage = await _context.Stages

                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudentIDhashed == idStudentStage);
            ViewBag.ham9atna = stage.EtatStage;
            if (stage != null)
            {
                return View(stage);
            }

            

            //------------------------------------
            PopulateDepartmentsDropDownList();
           ViewData["InstructorID"] = new SelectList(_context.Instructors , "ID", "FirstMidName");

            // var studentid = await _context.Students.FindAsync(id);
            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
             

            //ViewData["StudentIDhashed"] = userId;

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        /*, Genre, GIN, CNE, Email,DateNaissance, Annee,Telephone, OrganismeDacceuil, EncadrantExterne, EmailEncadrantExterne,TelephoneEncadrantExterne, PosteEncadrantExterne, TitreStage, DescriptionStage, VilleStage, PaysStage, DateDebutStage, DateFinStage, DepartmentID,InstructorID*/
        public async Task<IActionResult> CreateStage(Stage stage)
        {


            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stage);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return View();
                }
                PopulateDepartmentsDropDownList(stage.DepartmentID);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save Stage . " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
           return RedirectToAction(nameof(Index));
            
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
            Console.WriteLine(selectedDepartment);
        }



    }
}
