using CandidateAssessment.Models;
using CandidateAssessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CandidateAssessment.Controllers
{
    public class RecordsController : Controller
    {
        private StudentService _studentService;
        private SchoolService _schoolService;
        public RecordsController(StudentService studentService, SchoolService schoolService)
        {
            _studentService = studentService;
            _schoolService = schoolService;
        }

        public IActionResult Students()
        {
            ViewBag.AgeList = CreateAgeList();
            ViewBag.SchoolList = CreateSchoolDropdownList();
            ViewBag.OrgList = CreateStudentOrgDropdown();

            var model = _studentService.GetStudents().OrderBy(s => s.LastName);
            return View(model);
        }

        public IActionResult Schools()
        {
            var model = _schoolService.GetSchools().OrderBy(s => s.Name);
            return View(model);
        }

        public IActionResult SchoolStudents(School school)
        {
            var model = _schoolService.GetSchoolStudents(school.SchoolId).OrderBy(s => s.LastName);
            return View("_StudentTable", model);
        }

        [HttpPost]
        public IActionResult SaveStudent(Student model)
        {
            _studentService.SaveStudent(model);
            return RedirectToAction("Students");
        }

        private List<SelectListItem> CreateAgeList()
        {
            var ageList = new List<SelectListItem>();
            for (int i = 18; i < 100; i++)
            {
                ageList.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return ageList;
        }

        private List<SelectListItem> CreateSchoolDropdownList()
        {
            // replace this code with code to grab the schools and create a List<SelectListItem> object from them.
            var model = _schoolService.GetSchools();
            var options = model.Select(school => new SelectListItem
            {
                Text = school.Name,
                Value = school.SchoolId.ToString()
            }).ToList();
            return options;
        }

        private MultiSelectList CreateStudentOrgDropdown()
        {
            var model = _studentService.GetStudentsOrgs();
            // replace this code with code to grab the student orgs and create a List<SelectListItem> object from them.
            var options = model.Select(org => new SelectListItem
            {
                Text = org.OrgName,
                Value = org.Id.ToString() // Assuming Id is the property representing the value
            }).ToList();

            return new MultiSelectList(options, "Value", "Text");
        }
    }
}