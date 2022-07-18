using System;
using System.Collections.Generic;

namespace ApplicantTest.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public int SchoolId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }

        public virtual School School { get; set; } = null!;
    }
}
