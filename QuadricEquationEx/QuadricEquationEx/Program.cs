namespace QuadricEquationEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            double x1, x2,a,b,c;
            Console.WriteLine("Introduceti 3 valori de la tastatura:");
            a = double.Parse(Console.ReadLine());
            b = double.Parse(Console.ReadLine());
            c = double.Parse(Console.ReadLine());

            

            if (a == 0 && b == 0 && c == 0)
            {
                x1 = x2 = 0;
                Console.WriteLine($"x1 = {x1}" + $", x2 = {x2}");
            }
            else
            {
                double delta = (Math.Pow(b, 2) - (4 * a * c));

                if(delta > 0)
                {
                    x1 = ((-b + (Math.Sqrt(delta)))/ (2 * a));
                    x2 = ((-b - (Math.Sqrt(delta))) / (2 * a));
                    Console.WriteLine($"x1 = {x1}" + $", x2 = {x2}");
                }
                else if(delta < 0)
                {
                    x1 = (-b / (2 * a));
                    delta = -delta;
                    x2 = ((Math.Sqrt(delta)) /  (2 * a));
                    Console.WriteLine($"Nu exista solutii reale! Exista doua radacini complexe distincte: {x1} +/-  {x2}i");
                }
                else if(delta == 0)
                {
                    x1 = (-b / (2 * a));
                    Console.WriteLine($"Radacina dubla: {x1}");
                }
                
            }
            
        }
    }
}