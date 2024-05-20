using System.ComponentModel.DataAnnotations;

namespace StudentMG.ViewModels
{
    public class ImageVM
    {
        [Key]
        public int No { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public ImageVM() { }

        public ImageVM(int no, string name, string source)
        {
            No = no;
            Name = name;
            Source = source;
        }
    }
}
