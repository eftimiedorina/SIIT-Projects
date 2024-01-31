using System.Text;

namespace ObfuscateEmail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder email = new StringBuilder("dorinaelena@gmail.com");
            StringBuilder hideEmail = new StringBuilder();

            int i = 0;
            while (email[i] != '@')
            {
                hideEmail[i] = '*';
                i++;
            }

            hideEmail.Append(email.ToString(i,email.Length - i));

            Console.WriteLine(hideEmail.ToString());

        }
    }
}