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
using System.Windows.Shapes;
using System.IO;

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для ChangeTeamInfo.xaml
    /// </summary>
    public partial class ChangeTeamInfo : Window
    {
        List<Team> teams = new List<Team>();
        TeamWindow twd;

        public ChangeTeamInfo(TeamWindow m)
        {
            InitializeComponent();
            twd = m;
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            string name = twd.item;

            string newTeamName = teamName.Text;
            string nameOfCoach = coachName.Text;
            int teamWins = int.Parse(numWins.Text);
            int teamLoses = int.Parse(numLoses.Text);

            using (StreamReader sr = new StreamReader("../../Players.txt"))
            {
                while (!sr.EndOfStream)
                {
                    List<Player> players = new List<Player>();

                    players.Clear();
                    string teamName = sr.ReadLine();
                    string coachName = sr.ReadLine();
                    string[] teamStats = sr.ReadLine().Split('-');
                    int wins = int.Parse(teamStats[0]);
                    int loses = int.Parse(teamStats[1]);
                    if (teamName == name)
                    {
                        teamName = newTeamName;
                        coachName = nameOfCoach;
                        wins = teamWins;
                        loses = teamLoses;
                    }
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

                        players.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to));
                        if (sr.EndOfStream)
                            break;
                    }
                    teams.Add(new Team(teamName, coachName, wins, loses, players));
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
