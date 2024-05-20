using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMG.ViewModels
{
    public class GradeVM
    {
        [Key]
        public int No { get; set; }
        public string Name { get; set; }
        public int? NoCredits { get; set; }
        public int? Grade { get; set; }

        public GradeVM() {
        }

        public GradeVM(int No, string name, int? noCredits, int? grade)
        {
            Name = name;
            NoCredits = noCredits;
            Grade = grade;
        }
    }
}
