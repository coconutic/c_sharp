using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  University
{
    public class Student : IEquatable<Student>, IComparable<Student>
    {
        private string name;
        private string surname
        private double average_mark;
        private int group;

        public int Group
        {
            set { group = value; }
            get { return group; }
        }

        public string Name
        { 
            set { name = value; }
            get { return name; }
    
        }

        public string Surname
        { 
            set { surname = value; }
            get { return surname; }
    
        }

        public double Average_mark
        {
            set { if (value >= 0 && value <= 10) average_mark = value; }
            get { return average_mark; }
        }

        public override string ToString()
        {
            return "Name: " + name + "\tSurname: " + surname + "\tGroup: " + group + "\tAverage_mark: " + average_mark;
        }
        
        public bool Equals(Tovar sms)
        {
            if (sms == null) return false;
            if (this.Group == sms.Group)
                return true;
            else
                return false;
        }
        
        public int CompareTo(Student sms)
        {
            if (this.average_mark > sms.average_mark)
                return 1;
            else if (this.average_mark == sms.average_mark)
                return 0;
            else return -1;
        }        
    }
    public class Database
    {

        List <Student> stud = new List<Student>();

        public void inf()
        {
            foreach (Student person in stud)
            {
                Console.WriteLine(person);
            }
        }
        public void Add(Student stud)
        {
            pokupka.Add(stud);
        }

        public void Remove(Student stud)
        {
            pokupka.Remove(stud);
        }

        public int choose(int boolean){

            bool boolean = true;

            Console.WriteLine("_____   Choose the program:   _____");
            Console.WriteLine("1 - Add ");
            Console.WriteLine("2 - Remove ");
            Console.WriteLine("3 - Search ");
            Console.WriteLine("4 - EXIT ");
            Console.WriteLine("\n");

            while (boolean) 
            {
                string switcher = Console.ReadLine();
            switch (switcher)
            {
                case "1": 
                    Add();
                    break;
                case "2":
                    Remove();
                    break;
                case "3":
                    //Search();
                    break;
                case "4":
                    boolean = false;
                break;

                default:
                    Console.WriteLine("You're wrong, no-no-no!");
                break;
            }
            }
        }


        public void display_groups()
        {
            Console.WriteLine("________DATABASE________");
            inf();
            Console.WriteLine("________________________");
            choose();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Database dt = new Database();

            dt.Add(new Student() { Name = "Andrey", Surname = "Ivanov", Average_mark = 7,3 , Group = 453501});
            dt.Add(new Student() { Name = "Ekaterina", Surname = "Kurach", Average_mark = 6 , Group = 173501 });
            dt.Add(new Student() { Name = "Tanya", Surname = "Pavlova", Average_mark = 8,1 , Group = 453504 });
            dt.Add(new Student() { Name = "Vlad", Surname = "Sman", Average_mark = 5 , Group = 393503 });
            dt.Add(new Student() { Name = "Mark", Surname = "Matveeva", Average_mark = 9,1 , Group = 453502});

            dt.display_groups();

            Console.ReadKey();

        }
    }
}
