namespace ArrayAssignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dati dimensiunea unui vector:");
            int length = int.Parse(Console.ReadLine());
            Console.WriteLine($"Populati vectorul cu {length} elemente:");

            int min, max =0 ;
            int[] array = new int[length];

            min = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < length-1; i++)
            {
                array[i] = int.Parse(Console.ReadLine());

                if (min > array[i])
                {
                    min = array[i];
                }

                if (max < array[i])
                {
                    max = array[i];
                }

            }

            Console.WriteLine($"Min = {min}, Max = {max}");
        }
    }
}