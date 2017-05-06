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
        List<Team> teams = new List<Team>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void showPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            playerStats.Items.Clear();
            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                string item = rosterList.SelectedItem.ToString();
                while (!sr.EndOfStream)
                {
                    string teamName = sr.ReadLine();
                    string coachName = sr.ReadLine();
                    string[] teamStats = sr.ReadLine().Split('-');
                    int wins = int.Parse(teamStats[0]);
                    int loses = int.Parse(teamStats[1]);
                    sr.ReadLine();
                    while (true)
                    {
                        string playerName = sr.ReadLine();
                        if (playerName == "")
                        {
                            break;
                        }
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

                        Player player = new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to);
                        if (item.ToString() == playerName)
                        {
                            playerStats.Items.Add(player.PlayerInfo());
                        }
                        if (sr.EndOfStream)
                            break;
                    }
                    Team team = new Team(teamName, coachName, wins, loses, players);
                }
            }
        }

        private void editPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            editStats editStats = new editStats();
            editStats.Show();
        }

        private void deletePlayer_Click(object sender, RoutedEventArgs e)
        {
            teams.Clear();
            string item = rosterList.SelectedItem.ToString();

            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    List<Player> newPlayers = new List<Player>();
                    newPlayers.Clear();

                    string teamName = sr.ReadLine();
                    string coachName = sr.ReadLine();
                    string[] teamStats = sr.ReadLine().Split('-');
                    int wins = int.Parse(teamStats[0]);
                    int loses = int.Parse(teamStats[1]);
                    sr.ReadLine();
                    while (true)
                    {
                        string playerName = sr.ReadLine();
                        if (playerName == item)
                        {
                            sr.ReadLine();
                            sr.ReadLine();
                            sr.ReadLine();
                            playerName = sr.ReadLine();
                        }
                        if (playerName == "")
                        {
                            break;
                        }
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

                        newPlayers.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to));
                        if (sr.EndOfStream)
                            break;
                    }
                    teams.Add(new Team(teamName, coachName, wins, loses, newPlayers));
                }
            }
            using (StreamWriter sw = new StreamWriter("../../Players.txt"))
            {
                foreach (Team team in teams)
                {
                    sw.WriteLine(team.TeamInfoFile());
                }
            }
        }
    }
}
