using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace CSharp_Exam.Models
{
    public class Course : ICourse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public float FeePerStudent { get; set; }
        public int EnrolledCount { get; set; }

        public Course() { }

        public Course(int courseId, string courseName, float feePerStudent, int enrolledCount)
        {
            if (!IsValidCourseName(courseName))
                throw new ArgumentException("Course name must be between 3 and 100 characters.");
            if (feePerStudent <= 0)
                throw new ArgumentException("Fee per student must be greater than 0.");
            if (enrolledCount <= 0)
                throw new ArgumentException("Enrolled count must be greater than 0.");

            CourseId = courseId;
            CourseName = courseName;
            FeePerStudent = feePerStudent;
            EnrolledCount = enrolledCount;
        }

        private bool IsValidCourseName(string courseName)
        {
            return !string.IsNullOrWhiteSpace(courseName) && courseName.Length >= 3 && courseName.Length <= 100;
        }

        public virtual float CalculateTotalFee() => FeePerStudent * EnrolledCount;

        public virtual void DisplayCourseDetails()
        {
            Console.WriteLine($"Course Information: \n- Name: {CourseName} \n- Enrolled Students: {EnrolledCount} \n- Total Fee: ${CalculateTotalFee()}");
        }
    }
}

