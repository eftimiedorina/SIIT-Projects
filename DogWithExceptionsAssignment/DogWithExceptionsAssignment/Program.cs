namespace DogWithExceptionsAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                Dog dog = new Dog { Age = -1, Name = "R" };
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("ArgumentException: " + e.Message);
            }
            catch(ApplicationException e) 
            {
                Console.WriteLine("ApplicationException: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}