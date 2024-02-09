namespace ClassStudentAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student[] students = new Student[4];
            byte ct = 0;


            while (ct < 4)
            {
                Console.WriteLine("Enter age:");
                int age = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();

                try
                {
                    students[ct] = new Student(name, age);
                    ct++;
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            Console.WriteLine("All the students are:");
            foreach (Student student in students) 
            {
                Console.WriteLine(student.Info);
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Insert a mark for {students[i].Name}:");
                students[i].Mark = int.Parse(Console.ReadLine());


            }

            double average = 0;
            byte cnt = 0;
            for(int i = 0; i < students.Length; i++)
            {
                if (students[i].Mark != null )
                {
                    average = average + students[i].Mark.Value;
                    cnt++;
                }
            }

            Console.WriteLine($"The average mark is {average / cnt}");
        }
    }
}