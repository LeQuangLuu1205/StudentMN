namespace StudentMG.ViewModels
{
    public class ImageTempVM
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public ImageTempVM() { }

        public ImageTempVM(string name, string source)
        {
            Name = name;
            Source = source;
        }
    }
}
