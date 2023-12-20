namespace ArrayAssignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inserati dimensiunea vectorului:");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Inserati un vector de string-uri:");

            int min = 0;
            int max = 0;
            string sMin = null, sMax = null;
            string[] str = new string[n];
            
            for(int i = 0; i < n; i++) 
            {
                str[i] = Console.ReadLine();

                if(min > str[i].Length)
                {
                    min = str[i].Length;
                    sMin = str[i];
                }

                if(max < str[i].Length)
                {
                    max = str[i].Length;
                    sMax = str[i];
                }
            }

            Console.WriteLine($"Cel mai mic element este {sMin} cu {min} caractere");
            Console.WriteLine($"Cel mai mare element este {sMax} cu {max} caractere");
            
        }
    }
}