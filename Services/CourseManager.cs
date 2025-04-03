using CSharp_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Exam.Services
{
    public class CourseManager
    {
        private List<Course> courses = new List<Course>();

        public void RegisterCourse()
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                Console.Write("Enter Course Name: ");
                string courseName = Console.ReadLine();

                Console.Write("Enter Fee Per Student: ");
                float feePerStudent = float.Parse(Console.ReadLine());

                Console.Write("Enter Enrolled Students: ");
                int enrolledCount = int.Parse(Console.ReadLine());

                Console.Write("Is this an Online Course? (yes/no): ");
                bool isOnline = Console.ReadLine().Trim().ToLower() == "yes";

                Course course = isOnline
                    ? new OnlineCourse(courseId, courseName, feePerStudent, enrolledCount)
                    : new Course(courseId, courseName, feePerStudent, enrolledCount);

                courses.Add(course);
                Console.WriteLine("Course registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewAllCourses()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            foreach (var course in courses)
            {
                course.DisplayCourseDetails();
            }
        }

        public void UpdateCourseFee()
        {
            try
            {
                Console.Write("Enter Course ID to update: ");
                int courseId = int.Parse(Console.ReadLine());

                Course course = courses.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                Console.Write("Enter new Fee Per Student: ");
                float newFee = float.Parse(Console.ReadLine());

                if (newFee <= 0)
                {
                    Console.WriteLine("Fee must be greater than 0.");
                    return;
                }

                course.FeePerStudent = newFee;
                Console.WriteLine("Course fee updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteCourse()
        {
            try
            {
                Console.Write("Enter Course ID to delete: ");
                int courseId = int.Parse(Console.ReadLine());

                Course course = courses.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                courses.Remove(course);
                Console.WriteLine("Course deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void CalculateTotalFeeById()
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                Course course = courses.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                Console.WriteLine($"Total Fee for {course.CourseName}: ${course.CalculateTotalFee()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}