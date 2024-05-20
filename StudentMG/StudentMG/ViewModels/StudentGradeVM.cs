namespace StudentMG.ViewModels
{
    public class StudentGradeVM
    {
        public string Name { get; set; }
        public int? NoCredits { get; set; }
        public int? Grade { get; set; }
        public StudentGradeVM() { }

        public StudentGradeVM(string name, int? noCredits, int? grade)
        {
            Name = name;
            NoCredits = noCredits;
            Grade = grade;
        }
    }
}
