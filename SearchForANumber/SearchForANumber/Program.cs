namespace SearchForANumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dati un numar de la tastaura:");

            int[] array = new int[] { 12, 78, 9, 0, 34, 8, 7 };

            int val = int.Parse(Console.ReadLine());

            int index = SearchNumberInArray(array, val);

            if (index >= 0)
                Console.WriteLine($"Numarul {val} se gaseste la pozitia {index} in array");
            else
                Console.WriteLine($"Numarul {val} nu exista in array");
        }

        static int SearchNumberInArray(int[] array, int val)
        {
            for(int i = 0;i < array.Length; i++) 
            {
                if(val == array[i])
                    return i;

            }

            return -1;
        }
    }
}