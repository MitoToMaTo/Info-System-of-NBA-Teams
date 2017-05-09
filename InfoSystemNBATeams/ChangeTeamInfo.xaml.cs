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

            using (StreamWriter sw = new StreamWriter("../../Players.txt"))
            {
                foreach (Team team in twd.teams)
                {
                    if (team.Name == name)
                    {
                        team.Name = newTeamName;
                        team.NameOfCoach = nameOfCoach;
                        team.Wins = teamWins;
                        team.Loses = teamLoses;
                    }
                    sw.WriteLine(team.ShortTeamInfoFile());
                    foreach (Player player in team.Players)
                    {
                        sw.WriteLine(player.PlayerInfoFile());
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
