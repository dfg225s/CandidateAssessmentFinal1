using ApplicantTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTest.Services
{
    public class SchoolService
    {
        private ApplicantTestContext _dbContext;

        public SchoolService(ApplicantTestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<School> GetSchools()
        {
            return _dbContext.Schools
                .Include(s => s.Students);
        }
    }
}
