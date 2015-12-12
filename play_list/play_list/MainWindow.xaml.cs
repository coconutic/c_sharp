using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Threading;

namespace play_list
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Play_List> Items = new ObservableCollection<Play_List>();

        public MainWindow()
        {
            InitializeComponent();
            Tabs.ItemsSource = Items;
        }

        public void adds_click(object sender, RoutedEventArgs e)
        {
            var opensong = new OpenFileDialog();

            if (opensong.ShowDialog().Value)
            {
                Music song = new Music();
                song.Name1 = opensong.SafeFileName;
                song.Path1 = opensong.FileName;
                var current = Tabs.SelectedIndex;
                Items.ElementAt(current).add_song(song);
            }
        }

        public void delete_click(object sender, RoutedEventArgs e)
        {
            Play_List pl = (Play_List)Tabs.SelectedItem;
            pl.delete_song(pl.SelectedIndex);
        }

        public void play_click(object sender, RoutedEventArgs e)
        {
            Play_List current_pl = (Play_List)Tabs.SelectedItem;
            current_pl.play_click();
        }

        public void pause_click(object sender, RoutedEventArgs e)
        {
            Play_List current_pl = (Play_List)Tabs.SelectedItem;
            current_pl.pause_click();
        }

        public void save_click(object sender, RoutedEventArgs e)
        {
            int current = Tabs.SelectedIndex;
            Items.ElementAt(current).Save_playlist();
        }

        public void close_click(object sender, RoutedEventArgs e)
        {
            int current = Tabs.SelectedIndex;
            Items[current].deletepl();
            Items.RemoveAt(current);
        }

        public void add_click(object sender, RoutedEventArgs e)
        {
            Play_List npl = new Play_List();
            npl.Read_playlist();
            Items.Add(npl);
        }

    }

    public class Music
    {
        private int id;
        private string name;
        private int duration;
        private string path;
        private string singer;
        private int reiting;

        public Music()
        {
            ID1 = 0;
            Name1 = "unknow";
            Path1 = "unknow";
            Singer1 = "unknow";
            Reiting1 = 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID1
        {
            get { return id; }
            set { SetProperty(ref id, value, "ID"); }
        }
        public string Name1
        {
            get { return name; }
            set { SetProperty(ref name, value, "Name1"); }
        }
        public int Duration1
        {
            get
            {
                MediaPlayer mp = new MediaPlayer();
                mp.Open(new Uri(Path1));
                while (!mp.NaturalDuration.HasTimeSpan) { }
                int time = (int)mp.NaturalDuration.TimeSpan.TotalSeconds;
                mp.Close();
                return time;
            }

        }
        public string Path1
        {
            get { return path; }
            set { path = value; }
        }
        public string Singer1
        {
            get { return singer; }
            set { SetProperty(ref singer, value, "Singer1"); }
        }
        public int Reiting1
        {
            get { return reiting; }
            set { SetProperty(ref reiting, value, "Reiting1"); }
        }

        public void write(StreamWriter writer)
        {
            string s = ID1.ToString() + "#" + Name1.ToString() + "#"
                        + Singer1.ToString() + "#" + Reiting1.ToString() + '#' + Path1;
            writer.WriteLine(s);
        }

        public void read(StreamReader reader)
        {
            string s = reader.ReadLine();
            string[] info = new string[5];
            int current = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '#')
                {
                    current++;
                    continue;
                }
                info[current] += s[i];
            }
            ID1 = Int32.Parse(info[0]);
            Name1 = info[1];
            Singer1 = info[2];
            Reiting1 = Int32.Parse(info[3]);
            Path1 = info[4];
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

    public class Play_List
    {
        ObservableCollection<Music> play_l = new ObservableCollection<Music>();
        Thread current_pl;
        private int id;
        private string name;
        bool paused = false;
        bool to_delete = false;

        public Play_List()
        {
            ID = 0;
            Name = "Unknown";
            current_pl = new Thread(new ThreadStart(play));
            SelectedIndex = 0;
            current_pl.Start();
            paused = true;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Duration
        {
            get { return play_l.Sum(time => time.Duration1); }
        }
        public double Reiting
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < play_l.Count(); i++)
                {
                    sum += play_l[i].Reiting1;
                }

                return sum / (double)play_l.Count();
            }
        }
        public ObservableCollection<Music> Songs
        {
            get { return play_l; }
        }
        public int SelectedIndex { get; set; }

        public void add_song(Music song)
        {
            if (play_l.Count == 0)
            {
                song.ID1 = 1;
            } else
            {
                song.ID1 = play_l.ElementAt(play_l.Count - 1).ID1 + 1;
            }
            play_l.Add(song);
        }

        public void delete_song(int index)
        {
            play_l.RemoveAt(index);
        }

        private void play()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            while (play_l.Count == 0) { }

            while (true)
            {
                if (SelectedIndex >= play_l.Count)
                {
                    SelectedIndex = 0;
                }

                mediaPlayer.Open(new Uri(play_l[SelectedIndex].Path1));
                while (!mediaPlayer.NaturalDuration.HasTimeSpan) { }
                mediaPlayer.Play();
                while (mediaPlayer.Position < mediaPlayer.NaturalDuration.TimeSpan)
                {
                    if (to_delete)
                    {
                        mediaPlayer.Stop();
                        mediaPlayer.Close();
                        to_delete = false;
                        return;
                    }
                    if (paused)
                    {
                        TimeSpan position = mediaPlayer.Position;
                        mediaPlayer.Pause();
                        try
                        {
                            Thread.Sleep(Timeout.Infinite);
                        }
                        catch (Exception e)
                        {
                            mediaPlayer.Position = position;
                            mediaPlayer.Play();
                        }
                    }

                    if (new Uri(play_l[SelectedIndex].Path1) != mediaPlayer.Source)
                    {
                        mediaPlayer.Stop();
                        mediaPlayer.Close();
                        mediaPlayer.Open(new Uri(play_l[SelectedIndex].Path1));
                        while (!mediaPlayer.NaturalDuration.HasTimeSpan) { }
                        mediaPlayer.Play();
                    }
                }

                SelectedIndex++;
            }
        }

        public void play_click()
        {

            lock (this)
            {
                
            } if (paused)
                current_pl.Interrupt();
            paused = false;
        }

        public void pause_click()
        {
            paused = true;
        }

        public void Save_playlist()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog().Value)
            {
                StreamWriter writer = new StreamWriter(sfd.FileName);
                writer.WriteLine(ID);
                writer.WriteLine(Name);
                writer.WriteLine(play_l.Count);
                foreach (var song in play_l)
                {
                    song.write(writer);
                }
                writer.Dispose();
            }
        }

        public void Read_playlist()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog().Value)
            {
                StreamReader reader = new StreamReader(ofd.FileName);
                ID = Int32.Parse(reader.ReadLine());
                Name = reader.ReadLine();
                int n = Int32.Parse(reader.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    var song = new Music();
                    song.read(reader);
                    play_l.Add(song);
                }
                reader.Dispose();
            }
        }

        public void deletepl()
        {
            to_delete = true;
            try
            {
                if (paused)
                {
                    paused = false;
                    current_pl.Interrupt();
                }   
            }
            catch (Exception e)
            {

            }

            while (to_delete) { }
            current_pl.Abort();
        }
    }
}