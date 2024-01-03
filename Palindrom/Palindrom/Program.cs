using System.Text;

namespace Palindrom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("inserati un cuvant sau o propozitie:");
            
            string input = Console.ReadLine();
            

            int l=0,r = input.Length-1;
            bool isPalindrom = false;
            
        
             while( l <= r ) 
            {
                if (!(input[l] >= 'a' && input[l] <= 'z'))
                    l++;

                if (!(input[r] >= 'a' && input[r] <= 'z'))
                    r--;
                if (input[l] == input[r])
                {
                    isPalindrom = true;
                    l++;
                    r--;
                }
                else
                    break;
                    
                
            }
           
            Console.WriteLine(isPalindrom);


        }
    }
}