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
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            OrgAssignment orgAssignment = new OrgAssignment
            {
                StudentOrgId = student.SelectedOrgs[0],
                StudentId = student.StudentId
            };

            _dbContext.OrgAssignments.Add(orgAssignment);

            _dbContext.SaveChanges();
        }
    }
}
