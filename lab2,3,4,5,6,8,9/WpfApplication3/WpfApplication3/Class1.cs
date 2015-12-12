using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
    class Class1
    {
    public class Student : IEquatable<Student>, IComparable<Student>
    {
        private string name;
        private string surname;
        private int average_mark;
        private int group;
        private int age;

        public int Group { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Average_mark
        {
            set { if (value >= 0 && value <= 10) average_mark = value; }
            get { return average_mark; }
        }

        public override string ToString()
        {
            return "Name: " + name + "\tSurname:    " + surname + "\tGroup:  " + group + "\tAverage_mark:  "     + average_mark;
        }
        
        public bool Equals(Student sms)
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




    public class Groups
    {

        List <Student> stud = new List<Student>();

        public void inf()
        {
            for (int i = 0; i < stud.Count; i++)
            {
                Student person = stud[i];
                Console.WriteLine(person);
            }
        }

        public void Add(Student s1)
        {
            stud.Add(s1);
        }

        public void Remove(Student s1)
        {
            stud.Remove(s1);
        }

        public void search(string name, string surname){
            for (int i = 0; i < stud.Count; i++)
            {
                Student person = stud[i];
                if (person.Name == name && person.Surname == surname){
                   Console.WriteLine("Name: " + person.Name + "\tSurname: " + person.Surname + "\tGroup: " + person.Group + "\tAverage_mark: " + person.Average_mark);
                }
            }

        }

        public void remove_by_surname(string name, string surname){

            for (int i = 0; i < stud.Count; i++)
            {
                Student person = stud[i];
                if (person.Name.Equals(name) && person.Surname.Equals(surname)){
                    Remove(person);
                }
            }
        }
 
        public void compare_marks(){
            int mark = 0;
            int max = 0;
            for( int i = 0; i < stud.Count; i++){
                Student person = stud[i];
                if (person.Average_mark.CompareTo(mark) == -1){
                    max = mark;
                }
            }
            Console.WriteLine(max);
        }

        public void choose(){

            bool boolean = true;

            Console.WriteLine("_________________  Choose the program:   ______________");
            Console.WriteLine("1 - Add ");
            Console.WriteLine("2 - Remove ");
            Console.WriteLine("3 - Search ");
            Console.WriteLine("4 - Information ");
            Console.WriteLine("5 - Find the leader ");
            Console.WriteLine("6 - EXIT ");
            Console.WriteLine("\n");

            while (boolean) 
            {
                string switcher = Console.ReadLine();
            switch (switcher)
            {
                case "1": 
                    Student s1 = new Student();

                    Console.Write("Write a name: ");
                    s1.Name = Console.ReadLine();
                    Console.Write("Write a surname: ");
                    s1.Surname = Console.ReadLine();
                    Console.Write("Write an average mark: ");
                    string s = Console.ReadLine();
                    s1.Average_mark = Int32.Parse(s);
                    Console.Write("Write a group: ");
                    s = Console.ReadLine();
                    s1.Group = Int32.Parse(s);
                    Add(s1);
                    break;

                case "2":
                    Console.Write("Write a name: ");
                    string name = Console.ReadLine();
                    Console.Write("Write a surname: ");
                    string surname = Console.ReadLine();
                    remove_by_surname(name, surname);
                    break;

                case "3":
                    Console.Write("Write a name: ");
                    string name1 = Console.ReadLine();
                    Console.Write("Write a surname: ");
                    string surname1 = Console.ReadLine();
                    search(name1, surname1);
                    break;

                case "4":
                    inf();
                    break;
                case "5":
                    Console.Write("The leader: ");
                    compare_marks();
                    break;

                case "6":
                    boolean = false;
                break;

                default:
                    Console.WriteLine("You're wrong, be-be-be!");
                break;
            }
            }
        }


        public void display_groups()
        {
            Console.WriteLine("_________________DATABASE___________________");
            inf();
            Console.WriteLine("____________________________________________");
            choose();
        }

    }
 
  /*  class Program
    {
        static void Main(string[] args)
        {
            Groups dt = new Groups();

            dt.Add(new Student() { Name = "Andrey",    Surname = "Ivanov",    Average_mark = 7 , Group = 453501});
            dt.Add(new Student() { Name = "Ekaterina", Surname = "Kurach",    Average_mark = 6 , Group = 173501 });
            dt.Add(new Student() { Name = "Tanya",     Surname = "Pavlova",   Average_mark = 8 , Group = 453504 });
            dt.Add(new Student() { Name = "Vlad",      Surname = "Sman",      Average_mark = 5 , Group = 393503 });
            dt.Add(new Student() { Name = "Mark",      Surname = "Matveeva",  Average_mark = 9 , Group = 453502});

            dt.display_groups();

            Console.ReadKey();

        }
    }*/
}
    }

