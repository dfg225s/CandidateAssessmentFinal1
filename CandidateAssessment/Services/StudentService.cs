using CandidateAssessment.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CandidateAssessment.Services
{
    public class StudentService
    {
        private CandidateAssessmentContext _dbContext;

        public StudentService(CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students
                .Where(st => st.FirstName != null && st.LastName != null)
                .Include(s => s.School)
                .Include(s => s.OrgAssignments)
                    .ThenInclude(oa => oa.StudentOrg);
        }

        public IEnumerable<StudentOrganization> GetStudentsOrgs()
        {
            return _dbContext.StudentOrganizations.ToList();

        }
        public void SaveStudent(Student student)
        {
            if (student.SelectedOrgs != null && student.SelectedOrgs.Any())
            {
                // Save the student information
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();

                // Iterate over selected organization IDs
                foreach (int orgId in student.SelectedOrgs)
                {
                    OrgAssignment orgAssignment = new OrgAssignment
                    {
                        StudentOrgId = orgId,
                        StudentId = student.StudentId
                    };

                    // Save each OrgAssignment
                    _dbContext.OrgAssignments.Add(orgAssignment);
                }

                // Save changes to the database
                _dbContext.SaveChanges();
            }
        }
    }
}
