using Microsoft.AspNetCore.Mvc.Rendering;
using StudentMG.Data;

namespace StudentMG.ViewModels
{
    public class Student2VM
    {
        public Student Student { get; set; }
        public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();

        public Student2VM(Student student, List<SelectListItem> classes)
        {
            Student = student;
            Classes = classes;
        }

        public Student2VM()
        {
        }
    }
}
