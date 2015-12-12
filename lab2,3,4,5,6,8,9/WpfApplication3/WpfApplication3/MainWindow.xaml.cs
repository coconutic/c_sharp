using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.Collections; 

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> stud = new ObservableCollection<Student>();
        private List<List<string>> m = new List<List<string>>();
        public ObservableCollection<Student> Students { get { return stud; } }
        public MainWindow()
        {
            InitializeComponent();
            //foreach (var s in Repository.ReadAll())
            //{
            //    Students.Add(s);
            //}
            //DataContext = this;
            // Students.Add
            stud = new ObservableCollection<Student>(Repository.ReadAll());

            lst.ItemsSource = stud;


            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            lang.Items.Clear();
            foreach (var lang1 in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang1.DisplayName;
                menuLang.Tag = lang1;
                menuLang.IsChecked = lang1.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                lang.Items.Add(menuLang);
            }

            //            //Serialize Dictonary<,>=====================================================OK
            //Dictionary<int, string> a = new Dictionary<int, string>();
            //a[1] = "I";
            //a[2] = "Love";
            //a[3] = "с#";
            //StreamWriter writer = new StreamWriter("bububu1.txt");
            //var serializer = new MineSerializ(writer, true);
            //serializer.write_obj(a);
            //writer.Dispose();
 
            ////Deserialize Dictonary<,>===================================================Error
            ////KeyValuePair<K,V> K-constant, can't setproperty
            //a.Clear();
            //StreamReader reader = new StreamReader("bububu1.txt");
            //var deserializer = new MineSerializ(reader, false);
            //var temp = deserializer.read_obj(a);
            //foreach (var element in temp)
            //{
            //    a.Add((int)element, (string)element);
            //    MessageBox.Show(((int)element).ToString() + ((string)element).ToString());
            //}


            
            //  //Serialize List========================================================OK
            // List<string> mylist = new List<string>();
            // mylist.Add("I");
            // mylist.Add("love");
            // mylist.Add("c#");
            // StreamWriter writer = new StreamWriter("bububu1.txt");
            // MineSerializ ser = new MineSerializ(writer, true);
            // ser.write_obj(mylist);
            // writer.Dispose();
           
            //// Deserialize List======================================================OK
            // mylist.Clear();
            // StreamReader reader = new StreamReader("bububu1.txt");
            // var deserializer = new MineSerializ(reader, false);
            // var temp = deserializer.read_obj(mylist);
            // foreach (var element in temp)
            // {
            //     mylist.Add((string)element);
            //     MessageBox.Show(((string)element).ToString());
            // }
            
            //Serialize List<List<>> ===============================================OK
            //List<List<string>> mylist = new List<List<String>>{
            //    new List<String> {},
            //    new List<String> {},
            //    new List<String> {},
            //    new List<String> {},
            //};
 
            //List<string> temp = new List<string>();
            //temp.Add("I");
            //mylist[0].AddRange(temp);
            //temp.Add("Love");
            //mylist[1].AddRange(temp);
            //temp.Add("c#");
            //mylist[2].AddRange(temp);
            //StreamWriter writer = new StreamWriter("bububu1.txt");
            //MineSerializ ser = new MineSerializ(writer, true);
            //ser.write_obj(mylist);
            //writer.Dispose();
 
            ////Deserialize List<List<>>===============================================OK
            //mylist.Clear();
            //StreamReader reader = new StreamReader("bububu1.txt");
            //var deserializer = new MineSerializ(reader, false);
            //var temp1 = deserializer.read_obj(mylist);
            //foreach (var element in temp1)
            //{
            //    List<string> tempList = new List<string>();
            //    foreach (var element1 in (IList)element)
            //    {
            //        tempList.Add((string)element1);
            //        MessageBox.Show((string)element1);
            //    }
            //} 

            ////Serialize Stack<>======================================================OK          
            //// writes in reverse order, don't remember to reverse it back
            //Stack<string> s = new Stack<string>();
            //s.Push("I");
            //s.Push("Love");
            //s.Push("c#");
            //StreamWriter writer = new StreamWriter("bububu1.txt");
            //MineSerializ ser = new MineSerializ(writer, true);
            //ser.write_obj(s);
            //writer.Dispose();
 
            ////Deserialize List=======================================================OK
            //s.Clear();
            //StreamReader reader = new StreamReader("bububu1.txt");
            //var deserializer = new MineSerializ(reader, false);
            //var temp = deserializer.read_obj(s);
            //foreach (var element in temp)
            //{
            //    s.Push((string)element);
            //    MessageBox.Show(((string)element).ToString());
            //}
 
 

            ////Serialize LinkedList<>=================================================OK
            //LinkedList<string> mylist = new LinkedList<string>();
            //mylist.AddLast("I");
            //mylist.AddLast("love");
            //mylist.AddLast("c#");
            //StreamWriter writer = new StreamWriter("bububu1.txt");
            //MineSerializ ser = new MineSerializ(writer, true);
            //ser.write_obj(mylist);
            //writer.Dispose();
           
            ////Deserialize LinkedList==================================================OK
            //mylist.Clear();
            //StreamReader reader = new StreamReader("bububu1.txt");
            //var deserializer = new MineSerializ(reader, false);
            //var temp = deserializer.read_obj(mylist);
            //foreach (var element in temp)
            //{
            //    mylist.AddLast((string)element);
            //    MessageBox.Show(((string)element).ToString());
            //}
        }
        
        public void Executed_New(object sender, ExecutedRoutedEventArgs e)
        {
            stud.Remove((Student)lst.SelectedItem);
        }

        public void CanExecute_New(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in lang.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }

        }

        private void btnGroup1_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = lst.Items;
            view.SortDescriptions.Add(new SortDescription("Mark", ListSortDirection.Ascending));

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = lst.Items;
            while (view.GroupDescriptions.Count > 0)
            {
                view.GroupDescriptions.RemoveAt(0);
            }
        }
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = lst.Items;
            view.SortDescriptions.Add(new SortDescription("Group", ListSortDirection.Ascending));
        }
        private void btnSort_Click_2(object sender, RoutedEventArgs e)
        {
            ICollectionView view = lst.Items;
            var desc = new PropertyGroupDescription { PropertyName = "Group" };
            view.GroupDescriptions.Add(desc);
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = lst.Items;
            var desc = new PropertyGroupDescription { PropertyName = "Mark" };
            var desc1 = new PropertyGroupDescription { PropertyName = "Group" };
            view.GroupDescriptions.Add(desc);
            view.GroupDescriptions.Add(desc1);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            stud.Remove((Student)lst.SelectedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var r1 = new Student { Name = fio.Text, Mark = Int32.Parse(avr.Text), Group = Int32.Parse(num.Text), Age = Int32.Parse(ag.Text) };
            stud.Add(r1);

        }
        //4 лабораторная ( сохранить в текстовый, в бинарный файл, открыть текстовый,бинарный, сжать, разжать файл)
        private void Save_file_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("filename.txt");
            int counter = stud.Count;
            writer.WriteLine(counter);
            for (int i = 0; i < counter; i++)
            {
                stud[i].Write_student(writer);

            }
            writer.Dispose();
        }

        private void Open_file_Click(object sender, RoutedEventArgs e)
        {
            stud.Clear();
            StreamReader reader = new StreamReader("filename.txt");
            int counter = Int32.Parse(reader.ReadLine());
            for (int i = 0; i < counter; i++)
            {
                var student = new Student { Name = " ", Group = 0, Mark = 0, Age = 0 };
                student.Read_student(reader);
                stud.Add(student);
            }
            reader.Dispose();

        }

        private void Open_binfile_Click(object sender, RoutedEventArgs e)
        {
            stud.Clear();
            BinaryReader reader = new BinaryReader(File.Open("filename.bin", FileMode.Open));

            int counter = reader.ReadInt32();
            for (int i = 0; i < counter; i++)
            {
                var student = new Student { Name = " ", Group = 0, Mark = 0, Age = 0 };
                student.Read_student_bin(reader);
                stud.Add(student);
            }
            reader.Dispose();

        }

        private void Save_binfile_Click(object sender, RoutedEventArgs e)
        {
            BinaryWriter writer = new BinaryWriter(File.Open("filename.bin", FileMode.Create));
            int counter = stud.Count;
            writer.Write(counter);
            for (int i = 0; i < counter; i++)
            {
                stud[i].Write_student_bin(writer);

            }
            writer.Dispose();
        }

        private void deflate_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Binary files|*.bin";
            if (file.ShowDialog() == true)
            {
                string filename = file.FileName;
                filename = filename.Substring(0, filename.Length - 4);
                string cmpfilename = filename + ".cmp";
                filename += ".bin";
                FileStream originalfile = File.OpenRead(filename);
                FileStream compfile = File.Create(cmpfilename);
                DeflateStream cmp = new DeflateStream(compfile, CompressionMode.Compress);
                originalfile.CopyTo(compfile);
                originalfile.Dispose();
                compfile.Dispose();
                cmp.Dispose();
            }
        }

        private void undeflate_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Compressed files|*.cmp";
            if (file.ShowDialog() == true)
            {
                string filename = file.FileName;
                filename = filename.Substring(0, filename.Length - 4); //удаляет расширение(последние 4 символа)
                string decmpfilename = filename;
                filename += ".cmp";
                decmpfilename += ".bin";
                FileStream originalfile = File.OpenRead(filename);
                FileStream decompfile = File.Create(decmpfilename);
                DeflateStream cmp = new DeflateStream(decompfile, CompressionMode.Decompress);
                originalfile.CopyTo(decompfile);
                originalfile.Dispose();
                decompfile.Dispose();
                cmp.Dispose();

            }
        }

        // 6 лабораторная ( использование Linq to object, отложенные операции)
        private void try_Click(object sender, RoutedEventArgs e)
        {
            goodstd.Text = null;
            badstd.Text = null;

            var std1 = new ObservableCollection<Student>(stud);
            stud.Clear();

            var std = std1.OrderByDescending(Student => Student.Mark);
            var student1 = std.First();

            var blacklist = std.Where(x => x.Mark <= 4);
            foreach (var st in blacklist)
            {
                badstd.AppendText(st.Name + " " + st.Mark + "\n");
            }

            foreach (var st in std)
            {
                stud.Add(st);
            }


        }

        private void to_group_Click(object sender, RoutedEventArgs e)
        {
            grooop.Text = null;

            var den = new ObservableCollection<Student>(stud);
            stud.Clear();

            var std = den.GroupBy(Student => Student.Age);
            var std2 = den.GroupBy(Student => Student.Group);
            grooop.Text += std2.Count();

            foreach (var st in std)
            {
                foreach (var student in st)
                {
                    stud.Add(student);
                }
            }
        }

        private void del_avr_Click(object sender, RoutedEventArgs e)
        {
            count_st.Text = null;
            count_st.Text += stud.Count(student => student.Mark < 3);
        }

        // 5 лабораторная ( сериализация, десериализация)
        private void serial_avr_Click(object sender, RoutedEventArgs e)
        {

            StreamWriter writer = new StreamWriter("bububu1.txt");
            var serializer = new MineSerializ(writer, true);
            serializer.write_obj(stud);
            writer.Dispose();
        }

        private void deserial_avr_Click(object sender, RoutedEventArgs e)
        {
            StreamReader reader = new StreamReader("bububu1.txt");
            var serializer = new MineSerializ(reader, false);
            stud.Clear();
            var p = serializer.read_obj(stud);
            foreach (var element in p)
            {
                stud.Add((Student)element);
            }
            reader.Dispose();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



    }
    //******************************************************************** kitten
    //                  ^                             ^
    //                 /*\  - - - - - - - - - - -    /*\
    //               /                                  \
    //              |      _____              _____      |
    //              |     (__$__)            (__$__)     |
    //               \                 ~                /
    //                 \           -______-           /
    //                   \                          /
    //********************************************************************

    [DataContract]

    public class Student : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name;
        private double _mark;
        private int _group;
        private int _age;

        public Student()
        {
            _name = "unknown";
            _mark = 0;
            _group = 0;
            _age = 0;
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }
        [DataMember]
        public double Mark
        {
            get { return _mark; }
            set { SetProperty(ref _mark, value, "Mark"); }
        }
        [DataMember]
        public int Group
        {
            get { return _group; }
            set { SetProperty(ref _group, value, "Group"); }
        }
        [DataMember]
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value, "Age"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        return string.IsNullOrEmpty(Name) ? "Name cannot be empty" : null;
                    case "Mark":
                        return Mark < 0 ? "Incorrect Mark" : null;
                    case "Group":
                        return Group < 0 ? "Incorrect Group" : null;
                    case "Age":
                        return Age < 0 ? "Incorrect Age" : null;
                }
                return null;
            }
        }

        public void Write_student(StreamWriter writer)
        {
            string output = Age.ToString() + ' ' + Group.ToString() + ' ' + Mark.ToString() + ' ' + Name;
            writer.WriteLine(output);

        }

        public void Read_student(StreamReader reader)
        {
            string[] inf = new string[4];
            int curent = 0;
            string st = reader.ReadLine();
            for (int i = 0; i < st.Length; i++)
            {
                if (st[i] == ' ' && curent < 3)
                {
                    curent++;
                    continue;
                }
                inf[curent] += st[i];
            }
            Age = Int32.Parse(inf[0]);
            Group = Int32.Parse(inf[1]);
            Mark = Double.Parse(inf[2]);
            Name = inf[3];
        }

        public void Write_student_bin(BinaryWriter writer)
        {
            writer.Write(Age);
            writer.Write(Group);
            writer.Write(Mark);
            writer.Write(Name);

        }

        public void Read_student_bin(BinaryReader reader)
        {
            Age = reader.ReadInt32();
            Group = reader.ReadInt32();
            Mark = reader.ReadDouble();
            Name = reader.ReadString();
        }

        public string Error
        {
            get { return null; }
        }

        private void SetProperty<T>(ref T field, T value, string name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }




    }

    [ValueConversion(typeof(double), typeof(Brush))]
    public class DoubleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value > 18 ? Brushes.Blue : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // это конвертер для однонаправленной привязки
            return null;
        }

    }

    public static class Repository
    {

        public static List<Student> ReadAll()
        {
            var r1 = new Student { Name = "Курач Екатерина Владиславовна", Mark = 8, Group = 453504, Age = 18 };
            var r2 = new Student { Name = "Лазовская Дарья Вадимовна", Mark = 6, Group = 543412, Age = 20 };
            var r3 = new Student { Name = "Пронькин Максим Игоревич", Mark = 8, Group = 453502, Age = 18 };
            var r4 = new Student { Name = "Езерская Елена Вадимовна", Mark = 9, Group = 453504, Age = 18 };
            var r5 = new Student { Name = "Гончар Владислав Сергеевич", Mark = 2.5, Group = 392023, Age = 21 };
            var r6 = new Student { Name = "Долматович Алина Сергеевна", Mark = 8, Group = 453504, Age = 18 };
            var r7 = new Student { Name = "Кравцов Артем Андреевич", Mark = 5, Group = 392023, Age = 21 };
            var r8 = new Student { Name = "Ващилко Александр Игоревич", Mark = 7, Group = 543412, Age = 20 };
            var r9 = new Student { Name = "Щепнов Дмитрий Вадимович", Mark = 4, Group = 123456, Age = 22 };
            var r10 = new Student { Name = "Каржанец Анастасия Владиславовна", Mark = 5, Group = 392023, Age = 17 };
            var r11 = new Student { Name = "Гончар Владислав Андреевич", Mark = 4, Group = 392023, Age = 18 };
            var r12 = new Student { Name = "Толкач Сергей Игоревич", Mark = 6, Group = 392023, Age = 17 };
            var r13 = new Student { Name = "Сакович Александр Михайлович", Mark = 8, Group = 123456, Age = 22 };
            var r14 = new Student { Name = "Буза Иван Андреевич", Mark = 9, Group = 453504, Age = 18 };
            var r15 = new Student { Name = "Царюк Татьяна Игоревна", Mark = 10, Group = 123456, Age = 22 };
            var r16 = new Student { Name = "Кравцова Эля Марковна", Mark = 2.5, Group = 392023, Age = 21 };


            return new List<Student> { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16 };


        }
    }

   class MineSerializ
    {
        StreamReader reader;
        StreamWriter writer;
        public MineSerializ(object stream, bool isWriter)
        {
            if (isWriter)
                writer = (StreamWriter)stream;
            else
                reader = (StreamReader)stream;
        }
        public void write_obj(object r1)
        {
            if (r1.GetType().IsPrimitive || r1.GetType() == typeof(string))
            {
                writer.WriteLine("\t<" + r1.GetType().Name + ">" + r1
                                    + "</" + r1.GetType().Name + ">");
                return;
            }
            var serializable_prop = r1.GetType().GetProperties();
            writer.WriteLine("<" + r1.GetType().Name + ">");
            foreach (PropertyInfo prinfo in serializable_prop)
            {
                if (prinfo.Name == "Item" || prinfo.Name == "Error")
                {
                    continue;
                }
                if (prinfo.GetValue(r1, null).GetType().IsPrimitive ||
                    prinfo.GetValue(r1, null).GetType() == typeof(string))
                {
                    writer.WriteLine("\t<" + prinfo.Name + ">" + prinfo.GetValue(r1, null)
                                        + "</" + prinfo.Name + ">");
                }
                else
                {
                    writer.WriteLine("\t<" + prinfo.Name + ">");
                    write_obj(prinfo);
                    writer.WriteLine("\t</" + prinfo.Name + ">");
                }
            }
 
            foreach (FieldInfo prinfo in r1.GetType().GetFields(BindingFlags.Instance))
            {
 
                if (prinfo.GetValue(r1).GetType().IsPrimitive ||
                    prinfo.GetValue(r1).GetType() == typeof(string))
                {
                    writer.WriteLine("\t<" + prinfo.Name + ">" + prinfo.GetValue(r1)
                                        + "</" + prinfo.Name + ">");
                }
                else
                {
                    writer.WriteLine("\t<" + prinfo.Name + ">");
                    write_obj(prinfo);
                    writer.WriteLine("\t</" + prinfo.Name + ">");
                }
            }
 
            writer.WriteLine("</" + r1.GetType().Name + ">");
            writer.Flush();
        }
        public void write_obj(System.Collections.IEnumerable r1)
        {
            writer.WriteLine("<" + r1.GetType().Name + ">");
            int count = 0;
            foreach (var element in r1)
            {
                count++;
            }
            writer.WriteLine("<Count>{0}</Count>", count);
 
            foreach (var element in r1)
            {
                if (element is System.Collections.IEnumerable && element.GetType() != typeof(string))
                {
                    write_obj((System.Collections.IEnumerable)element);
                }
                else
                {
                    write_obj(element);
                }
            }
            writer.WriteLine("</" + r1.GetType().Name + ">");
            writer.Flush();
        }
 
 
        public object read_obj(object r1)
        {
 
            Type type = r1.GetType();
            string s = reader.ReadLine();
            var parsed = Parse(s);
            if (parsed.Item1 != type.Name.ToString())
            {
                throw new Exception();
            }
 
            if (type.IsPrimitive || type == typeof(string))
            {
                return Convert.ChangeType(parsed.Item2, type);
            }
 
            var serializable_prop = r1.GetType().GetProperties();
            foreach (PropertyInfo prinfo in serializable_prop)
            {
                if (prinfo.Name == "Item" || prinfo.Name == "Error")
                {
                    continue;
                }
                if (prinfo.GetValue(r1, null).GetType().IsPrimitive ||
                    prinfo.GetValue(r1, null).GetType() == typeof(string))
                {
                    string t = reader.ReadLine();
                    var p = Parse(t);
                    if (p.Item1 != prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                    if (prinfo.CanWrite)       //not have set
                        prinfo.SetValue(r1, Convert.ChangeType(p.Item2, prinfo.PropertyType));  //set value to prinfo in r1
                } else
                {
                    string t = reader.ReadLine();
                    var p = Parse(t);
                    if (p.Item1 != prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                    var act = prinfo.GetType();
                    var obj = Activator.CreateInstance(act);
                    prinfo.SetValue(r1, read_obj(obj));
                    t = reader.ReadLine();
                    p = Parse(t);
                    if (p.Item1 != "/" + prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                }
            }
 
            foreach (FieldInfo prinfo in r1.GetType().GetFields(BindingFlags.Instance))
            {
 
                if (prinfo.GetValue(r1).GetType().IsPrimitive ||
                    prinfo.GetValue(r1).GetType() == typeof(string))
                {
                    string t = reader.ReadLine();
                    var p = Parse(t);
                    if (p.Item1 != prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                    prinfo.SetValue(r1, Convert.ChangeType(p.Item2, prinfo.GetType()));
                }
                else
                {
                    string t = reader.ReadLine();
                    var p = Parse(t);
                    if (p.Item1 != prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                    var act = prinfo.GetType();
                    var obj = Activator.CreateInstance(act);
                    prinfo.SetValue(r1, read_obj(obj));
                    t = reader.ReadLine();
                    p = Parse(t);
                    if (p.Item1 != "/" + prinfo.Name.ToString())
                    {
                        throw new Exception();
                    }
                }
            }
 
            s = reader.ReadLine();
            if (Parse(s).Item1 != "/" + type.Name.ToString())
            {
                throw new Exception();
            }
 
            return r1;
        }
 
        public System.Collections.IEnumerable read_obj(System.Collections.IEnumerable r1)
        {
            Type type;
            if (r1.GetType().GetGenericArguments().Length != 1)  
            {
                type = typeof(KeyValuePair<,>).MakeGenericType(
                    r1.GetType().GetGenericArguments()[0],
                    r1.GetType().GetGenericArguments()[1]);
            } else {
                type = r1.GetType().GetGenericArguments()[0];
            }
 
            reader.ReadLine();
            var s = reader.ReadLine();
            var property = Parse(s);
            if (property.Item1 != "Count")
            {
                throw new Exception();
            }
 
            Type genericListType = typeof(List<>).MakeGenericType(type);
            //List<type>
            var res = (IList)Activator.CreateInstance(genericListType);
            for (int i = 0; i < Int32.Parse(property.Item2); i++)
            {
                bool isString = false;
                if (type.FullName == "System.String") { // There is no standart constructor for System.String
                    isString = true;  
                }
                var myObject = isString ? "" : Activator.CreateInstance(type);
                // if isString == true, Create empty string, else create genericTypeObject
                if (myObject is System.Collections.IEnumerable && myObject.GetType() != typeof(string))
                {
                    myObject = read_obj((System.Collections.IEnumerable)myObject);
                }
                else
                {
                    myObject = read_obj(myObject);
                }
 
                res.Add(myObject);
            }
 
            s = reader.ReadLine();
            if (Parse(s).Item1 != "/" + r1.GetType().Name)
            {
                throw new Exception();
            }
            return (System.Collections.IEnumerable)res;
        }
 
 
        public Tuple<string, string> Parse(String s)
        {
            String name_prop = "";
            String value = "";
            bool tr = false;
 
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '>')
                {
                    tr = true;
                    continue;
                }
                if (s[i] == '<')
                {
                    tr = false;
                    continue;
                }
                if (tr == true) value += s[i];
            }
            tr = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '>')
                {
                    break;
                }
                if (s[i] == '<')
                {
                    tr = false;
                    continue;
                }
                if (tr == false) name_prop += s[i];
            }
 
            Tuple<string, string> result = new Tuple<string, string>(name_prop, value);
            return result;
        }
    }
}
