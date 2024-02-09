using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassStudentAssignment
{
    class Student
    {
        private string _name;
        private int _age;

        public const int MIN_AGE = 18;
        public const int MAX_AGE = 99;

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int Age
        {
            get { return _age; }
            set 
            {
                if(value < MIN_AGE)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Age must be at least {MIN_AGE}");
                else if(value > MAX_AGE)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Age cannot exceed {MAX_AGE}");
                else
                {
                    _age = value;
                }

            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int? Mark
        {
            get; set;
        }

        public string Info
        {
            get { return _name + " " + _age; }
        }
    }
}
