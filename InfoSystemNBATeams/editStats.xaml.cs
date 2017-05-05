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

// СВИСТОПЛЯСКА С МАССИВАМИ players; разобраться с ними

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для editStats.xaml
    /// </summary>
    public partial class editStats : Window
    {

        List<Team> teams = new List<Team>();

        public editStats()
        {
            InitializeComponent();
        }

        private void createNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            teams.Clear();

            string nameOfPlayer = name.Text;
            int numOfPlayer = int.Parse(number.Text);
            string pos = position.Text;
            string[] mass1 = heightWeight.Text.Split(';');
            int playerGrowth = int.Parse(mass1[0]);
            int playerWeight = int.Parse(mass1[1]);
            int draftYear = int.Parse(yearOfDraft.Text);
            string[] mass2 = ptsRbsAst.Text.Split(';');
            double points = double.Parse(mass2[0]);
            double rebounds = double.Parse(mass2[1]);
            double assists = double.Parse(mass2[2]);
            string[] mass3 = blkStlTo.Text.Split(';');
            double steals = double.Parse(mass3[0]);
            double blocks = double.Parse(mass3[1]);
            double turnovers = double.Parse(mass3[2]);
            string[] mass4 = fgFt3pt.Text.Split(';');
            double fgPercentage = double.Parse(mass4[0]);
            double ftPercentage = double.Parse(mass4[1]);
            double threePtPercentage = double.Parse(mass4[2]);

            string nameOfTeam = teamName.Text; 

            Player player = new Player(nameOfPlayer, numOfPlayer, pos, playerGrowth, playerWeight, draftYear, points, rebounds, assists, steals, blocks, fgPercentage, ftPercentage, threePtPercentage, turnovers);

            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    List<Player> players = new List<Player>();

                    players.Clear();
                    string teamName = sr.ReadLine();
                    if (nameOfTeam == teamName)
                    {
                        players.Add(player);
                    }
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
