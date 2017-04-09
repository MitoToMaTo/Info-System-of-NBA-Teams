using System;
using System.Collections.Generic;
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
using System.IO; // Не забыть прописать!!!!

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Player> players = new List<Player>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void readTxt_Click(object sender, RoutedEventArgs e)
        {
            rosterList.Items.Clear();
            players.Clear();

            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string playerName = sr.ReadLine();
                    string [] orgInfo = sr.ReadLine().Split(',');
                    int num = int.Parse(orgInfo[0]);
                    string position = orgInfo[1];
                    int growth = int.Parse(orgInfo[2]);
                    int weight = int.Parse(orgInfo[3]);
                    int yearOfDraft = int.Parse(sr.ReadLine());
                    string[] stats = sr.ReadLine().Split(' ');
                    double pts = double.Parse(stats[0]);
                    double rbs = double.Parse(stats[1]);
                    double ast = double.Parse(stats[2]);
                    double stl = double.Parse(stats[3]);
                    double blk = double.Parse(stats[4]);
                    double to = double.Parse(stats[5]);
                    double fg = double.Parse(stats[6]);
                    double ft = double.Parse(stats[7]);
                    double tp = double.Parse(stats[8]);

                    sr.ReadLine();
                    players.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg,ft, tp, to));
                }
            }
            foreach (Player player in players)
            {
                rosterList.Items.Add(player.Name);
            }
        }

        private void showPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            playerStats.Items.Clear();
            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                string item = rosterList.SelectedItem.ToString();
                
                while (!sr.EndOfStream)
                {
                        string playerName = sr.ReadLine();
                        string[] orgInfo = sr.ReadLine().Split(',');
                        int num = int.Parse(orgInfo[0]);
                        string position = orgInfo[1];
                        int growth = int.Parse(orgInfo[2]);
                        int weight = int.Parse(orgInfo[3]);
                        int yearOfDraft = int.Parse(sr.ReadLine());
                        string[] stats = sr.ReadLine().Split(' ');
                        double pts = double.Parse(stats[0]);
                        double rbs = double.Parse(stats[1]);
                        double ast = double.Parse(stats[2]);
                        double stl = double.Parse(stats[3]);
                        double blk = double.Parse(stats[4]);
                        double to = double.Parse(stats[5]);
                        double fg = double.Parse(stats[6]);
                        double ft = double.Parse(stats[7]);
                        double tp = double.Parse(stats[8]);
                    sr.ReadLine();

                        Player player = new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to);
                        if (item.ToString() == playerName)
                        {
                            playerStats.Items.Add(player.PlayerInfo());
                        }
                }
                
            }
        }
    }
}
