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
using System.IO;
using System.Xml.Serialization;

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        public List<Team> teams = new List<Team>();
        public string item;
        public XmlSerializer formatter = new XmlSerializer(typeof(List<Team>));

        public TeamPage()
        {
            InitializeComponent();
        }
        public void readTXT_Click(object sender, RoutedEventArgs e)
        {
            teamList.Items.Clear();
            teams.Clear();

            if (File.Exists("../../Players.txt"))
            {
                if (File.Exists("../../Players.xml"))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
                        {
                            while (!sr.EndOfStream)
                            {
                                List<Player> players = new List<Player>();

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
                                    Player p = new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to);

                                    players.Add(p);
                                    if (sr.EndOfStream)
                                        break;
                                }
                                Team t = new Team(teamName, coachName, wins, loses, players);
                                teams.Add(t);
                            }
                        }
                        foreach (Team team in teams)
                        {
                            teamList.Items.Add(team.Name);
                        }

                        using (FileStream fs = new FileStream("../../Players.xml", FileMode.Create))
                        {
                            formatter.Serialize(fs, teams);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " Проверьте корректность данных, введенных в файл. ");
                    }
                }
                else
                {
                    MessageBox.Show(" Указан несуществующий путь xml файла. ");
                }
            }
            else
            {
                MessageBox.Show(" Указан несуществующий путь текстового файла. ");
            }
        }

        private void showTeamInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                teamRoster.Items.Clear();
                if (teamList.SelectedItem == null)
                {
                    MessageBox.Show(" Выберите команду, для которой будет показана информация. ");
                    return;
                }
                else
                {
                    item = teamList.SelectedItem.ToString();
                }
                foreach (Team t in teams)
                {
                    if (item == t.Name)
                    {
                        teamRoster.Items.Add(t.TeamInfo());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void changeTeamInfo_Click(object sender, RoutedEventArgs e)
        {
            if (teamList.SelectedItem == null)
            {
                MessageBox.Show(" Выберите команду, информация о которой будет изменена ");
                return;
            }
            else
            {
                item = teamList.SelectedItem.ToString();

                Pages.ChangeTeamInfoPage.Method1(item);
                Pages.ChangeTeamInfoPage.Method2(teams);
                Pages.ChangeTeamInfoPage.Method3(formatter);
            }

            NavigationService.Navigate(Pages.ChangeTeamInfoPage);

            if (File.Exists("../../Players.xml"))
            {
                try
                {
                    using (FileStream fs = new FileStream("../../Players.xml", FileMode.Open))
                    {
                        List<Team> newTeams = (List<Team>)formatter.Deserialize(fs);
                        foreach (Team team in newTeams)
                        {
                            if (team.Name == item)
                            {
                                Pages.ChangeTeamInfoPage.teamName.Text = team.Name;
                                Pages.ChangeTeamInfoPage.coachName.Text = team.NameOfCoach;
                                Pages.ChangeTeamInfoPage.numWins.Text = team.Wins.ToString();
                                Pages.ChangeTeamInfoPage.numLoses.Text = team.Loses.ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(" Указан несуществующий путь. ");
            }
        }

        private void showTeamRoster_Click(object sender, RoutedEventArgs e)
        {
            Pages.RosterPage.rosterList.Items.Clear();
            Pages.RosterPage.playerStats.Items.Clear();
            if (teamList.SelectedItem == null)
            {
                MessageBox.Show(" Выберите команду, для которой будет показан состав. ");
                return;
            }
            else
            {
                item = teamList.SelectedItem.ToString();
                Pages.RosterPage.Method1(item);
                Pages.RosterPage.Method2(teams);
                Pages.RosterPage.Method3(formatter);
            }

            foreach (Team t in teams)
            {
                if (item == t.Name)
                {
                    NavigationService.Navigate(Pages.RosterPage);
                    foreach (Player player in t.Players)
                    {
                        Pages.RosterPage.rosterList.Items.Add(player.Name);
                    }
                }
            }
        }

        public void search_Click(object sender, RoutedEventArgs e)
        {
            teamRoster.Items.Clear();
            if (teams.Count == 0)
            {
                MessageBox.Show(" Необходимо считать информацию из txt файла. ");
                return;
            }
            try
            {
                searchBox.Text = Pages.FirstUpper(searchBox.Text);

                Pages.RosterPage.rosterList.Items.Clear();
                Pages.RosterPage.playerStats.Items.Clear();
                bool a = false;
                foreach (Team team in teams)
                {
                    if (searchBox.Text == team.Name)
                    {
                        teamList.SelectedItem = searchBox.Text;
                        teamRoster.Items.Add(team.TeamInfo());
                        teamList.ScrollIntoView(teamList.SelectedItem);
                        a = true;
                        break;
                    }
                    foreach (Player player in team.Players)
                    {
                        if (searchBox.Text == player.Name)
                        {
                            teamList.SelectedItem = team.Name;
                            teamList.ScrollIntoView(teamList.SelectedItem);
                            NavigationService.Navigate(Pages.RosterPage);
                            item = team.Name;
                            foreach (Player newPlayer in team.Players)
                            {
                                Pages.RosterPage.rosterList.Items.Add(newPlayer.Name);
                            }
                            Pages.RosterPage.rosterList.SelectedItem = player.Name;
                            Pages.RosterPage.playerStats.Items.Add(player.PlayerInfo());
                            a = true;
                            break;
                        }
                    }
                }
                if (!a)
                {
                    MessageBox.Show(" Нет такой команды и такого игрока. ");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
