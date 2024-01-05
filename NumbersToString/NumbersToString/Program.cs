namespace NumbersToString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] unitati = {"", "one ", "two ", "three ", "four ", "five ", "six ", "seven ", "eight ", "nine " };
            string[] zeci ={ " ", "eleven ", "twelve ", "thirteen ", "fourteen ", "fifteen ",
                        "sixteen ", "seventeen ", "eighteen ","nineteen " };
            string[] zecimale = {"","ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
            string rezultat = "";
           // string sutime = "hundred",  mii= "thousand";

            Console.WriteLine("Dati un numar intre 0-9999:");
            
            int numar = int.Parse(Console.ReadLine());

            if (numar == 0)
                Console.WriteLine("Zero");
            if(numar / 1000 > 0)
            {
                rezultat += unitati[numar / 1000] + " thousand ";
                numar %= 1000;
            }

            if(numar /100 > 0)
            {
                rezultat += unitati[numar / 100] + " hundred ";
                numar %= 100;
            }

            if(numar > 0)
            {
                if(numar >=10 && numar <=19)
                {
                    rezultat += zeci[numar % 10];
                }
                else
                {
                    rezultat += zecimale[numar / 10] + " " + unitati[numar % 10];
                }

                Console.WriteLine(rezultat);
            }
                
        }
    }
}