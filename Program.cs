using CSharp_Exam.Services;

namespace CSharp_Exam
{
    class Program
    {
        static void Main()
        {
            CourseManager manager = new CourseManager();
            while (true)
            {
                Console.WriteLine("\n=== COURSE ENROLLMENT MENU ===");
                Console.WriteLine("1. Register New Course / Online Course");
                Console.WriteLine("2. View All Courses");
                Console.WriteLine("3. Update Course Fee");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Calculate Total Fee by Course ID");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                switch (Console.ReadLine())
                {
                    case "1": manager.RegisterCourse(); break;
                    case "2": manager.ViewAllCourses(); break;
                    case "3": manager.UpdateCourseFee(); break;
                    case "4": manager.DeleteCourse(); break;
                    case "5": manager.CalculateTotalFeeById(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }
    }
}