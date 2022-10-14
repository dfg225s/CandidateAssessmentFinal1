using ApplicantTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTest.Services
{
    public class StudentService
    {
        private ApplicantTestContext _dbContext;

        public StudentService (ApplicantTestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students
                .Include(s => s.School)
                .Include(s => s.OrgAssignments)
                    .ThenInclude(oa => oa.StudentOrg);
        }
    }
}
