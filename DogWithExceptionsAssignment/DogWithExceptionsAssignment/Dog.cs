using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWithExceptionsAssignment
{
    class Dog
    {
        private int age;
        private string name;


        public int Age
        {
            get { return age; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Age cannot be negative");
                if (value > 100)
                    throw new ApplicationException("Age cannot be greater than 100");
                else
                   age = value; 
            }
        }

        public string Name
        {
            get { return name; }
            set 
            {
                if(value.Length < 2)
                 throw new ApplicationException("Name length must be at least 2 characters"); 
                name = value; 
            }
        }
    }
}
