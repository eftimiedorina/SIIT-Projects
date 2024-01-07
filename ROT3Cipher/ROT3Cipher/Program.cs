using System.Text;

namespace ROT3Cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();
            StringBuilder sb = new StringBuilder();

            foreach(char c in input) 
            {
                if(char.IsLetter(c))
                {
                   char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    sb.Append((char)((c - baseChar + 3) % 26 + baseChar));
                }
                else 
                {
                    sb.Append(c);
                }
            }

            Console.WriteLine($"ROT3 equivalent: {sb}");
            
        }
    }
}