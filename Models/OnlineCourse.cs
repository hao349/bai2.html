using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Exam.Models
{
    public class OnlineCourse : Course
    {
        private const float PlatformFee = 10.0f;

        public OnlineCourse() { }

        public OnlineCourse(int courseId, string courseName, float feePerStudent, int enrolledCount)
            : base(courseId, courseName, feePerStudent, enrolledCount) { }

        public override float CalculateTotalFee() => (FeePerStudent + PlatformFee) * EnrolledCount;

        public override void DisplayCourseDetails()
        {
            Console.WriteLine($"Online Course Information: \n- Name: {CourseName} \n- Enrolled Students: {EnrolledCount} \n- Total Fee: ${CalculateTotalFee()} (Includes platform fee)");
        }
    }
}
