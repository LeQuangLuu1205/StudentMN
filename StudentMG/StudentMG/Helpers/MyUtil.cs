using System.Text;

namespace StudentMG.Helpers
{
    public class MyUtil
    {
        public static string GenerateRandomkey(int length = 5)
        {
            var pattern = @"lequangluutpqt@gmail.comksdjkfsdjkfiKJKSDF";
            var sb = new StringBuilder();
            var rd = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
    }
}
