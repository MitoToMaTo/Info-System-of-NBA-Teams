using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Serialization;

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для ChangeTeamInfoPage.xaml
    /// </summary>
    public partial class ChangeTeamInfoPage : Page
    {
        string name;
        string coach;
        List<Team> prevTeams;
        XmlSerializer newFormatter;

        public string Method0(string str)
        {
            coach = str;
            return coach;
        }
        public string Method1(string sr)
        {
            name = sr;
            return name;
        }

        public List<Team> Method2(List<Team> tms)
        {
            prevTeams = tms;
            return prevTeams;
        }

        public XmlSerializer Method3(XmlSerializer xmls)
        {
            newFormatter = xmls;
            return newFormatter;
        }

        public ChangeTeamInfoPage()
        {
            InitializeComponent();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pages.TeamPage.teamRoster.Items.Clear();
                if ((String.IsNullOrWhiteSpace(teamName.Text)) || (Regex.IsMatch(teamName.Text, @"[А-Яа-я]")) || (String.IsNullOrWhiteSpace(coachName.Text)) || (Regex.IsMatch(coachName.Text, @"[А-Яа-я]")) || (String.IsNullOrWhiteSpace(numWins.Text)) || (String.IsNullOrWhiteSpace(numLoses.Text)))
                {
                    MessageBox.Show(" Недопустимы русские буквы и пустые поля. ");
                    return;
                }

                string newTeamName = Pages.FirstUpper(teamName.Text);
                foreach (Team team in prevTeams)
                {
                    if (newTeamName != name)
                    {
                        if (newTeamName == team.Name)
                        {
                            MessageBox.Show(" Недопустимо 2 одинаковых названия команд. ");
                            teamName.Clear();
                            return;
                        }
                    }
                }
                string nameOfCoach = Pages.FirstUpper(coachName.Text);
                foreach (Team team in prevTeams)
                {
                    if (nameOfCoach != coach)
                    {
                        if (nameOfCoach == team.NameOfCoach)
                        {
                            MessageBox.Show(" Этот тренер уже тренирует одну из команд. ");
                            coachName.Clear();
                            return;
                        }
                    }
                }
                int teamWins = int.Parse(numWins.Text);
                int teamLoses = int.Parse(numLoses.Text);

                if (File.Exists("../../Players.txt"))
                {
                    using (StreamWriter sw = new StreamWriter("../../Players.txt"))
                    {
                        foreach (Team team in prevTeams)
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
                else
                {
                    MessageBox.Show(" Указан несуществующий путь. ");
                }

                if (File.Exists("../../Players.xml"))
                {
                    foreach (Team team in prevTeams)
                    {
                        if (name == team.Name)
                        {
                            team.Name = newTeamName;
                            team.NameOfCoach = nameOfCoach;
                            team.Wins = teamWins;
                            team.Loses = teamLoses;
                        }
                    }
                    using (FileStream fs = new FileStream("../../Players.xml", FileMode.Open))
                    {
                        newFormatter.Serialize(fs, prevTeams);
                    }
                    Pages.TeamPage.readTXT_Click(this, e);
                    NavigationService.Navigate(Pages.TeamPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.TeamPage);
            Pages.TeamPage.readTXT_Click(this, e);
            Pages.TeamPage.searchBox.Clear();
        }
    }
}
