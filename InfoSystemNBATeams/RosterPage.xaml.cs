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
using System.Xml.Serialization;

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для RosterPage.xaml
    /// </summary>
    public partial class RosterPage : Page
    {
        public string item;
        public List<Team> prevTeams;
        public XmlSerializer newFormatter;
        public string prevItem;

        public string Method1(string sr)
        {
            prevItem = sr;
            return prevItem;
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

        public RosterPage()
        {
            InitializeComponent();
        }

        public void updateTeamRoster()
        {
            foreach (Team t in prevTeams)
            {
                if (prevItem == t.Name)
                {
                    foreach (Player player in t.Players)
                    {
                        rosterList.Items.Add(player.Name);
                    }
                }
            }
        }

        private void showPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            playerStats.Items.Clear();
            if (rosterList.SelectedItem == null)
            {
                MessageBox.Show(" Выберите игрока, для которого будет показана информация. ");
                return;
            }
            else
            {
                item = rosterList.SelectedItem.ToString();
            }
            try
            {
                foreach (Team team in prevTeams)
                {
                    foreach (Player player in team.Players)
                    {
                        if (item == player.Name)
                        {
                            playerStats.Items.Add(player.PlayerInfo());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            Pages.CreatePlayerPage.name.Clear();
            Pages.CreatePlayerPage.number.Clear();
            Pages.CreatePlayerPage.position.Clear();
            Pages.CreatePlayerPage.heightWeight.Clear();
            Pages.CreatePlayerPage.yearOfDraft.Clear();
            Pages.CreatePlayerPage.ptsRbsAst.Clear();
            Pages.CreatePlayerPage.blkStlTo.Clear();
            Pages.CreatePlayerPage.fgFt3pt.Clear();

            NavigationService.Navigate(Pages.CreatePlayerPage);
            Pages.CreatePlayerPage.teamName.Text = prevItem;
            Pages.TeamPage.teamRoster.Items.Clear();
            Pages.CreatePlayerPage.Method1(item);
            Pages.CreatePlayerPage.Method2(prevTeams);
            Pages.CreatePlayerPage.Method3(newFormatter);

        }


        private void deletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (rosterList.SelectedItem == null)
            {
                MessageBox.Show(" Выберите игрока, которого хотите удалить. ");
                return;
            }
            else
            {
                item = rosterList.SelectedItem.ToString();
                Pages.DeletePlayerPage.Method1(item);
                Pages.DeletePlayerPage.Method2(prevTeams);
                Pages.DeletePlayerPage.Method3(newFormatter);
            }
            NavigationService.Navigate(Pages.DeletePlayerPage);
            Pages.TeamPage.teamRoster.Items.Clear();
        }


        private void changePlayerStats_Click(object sender, RoutedEventArgs e)
        {
            if (rosterList.SelectedItem == null)
            {
                MessageBox.Show(" Выберите игрока, информацию которого хотите поменять. ");
                return;
            }
            else
            {
                item = rosterList.SelectedItem.ToString();
                Pages.ChangeStatsPage.Method1(item);
                Pages.ChangeStatsPage.Method2(prevTeams);
                Pages.ChangeStatsPage.Method3(newFormatter);
            }

            NavigationService.Navigate(Pages.ChangeStatsPage);
            Pages.TeamPage.teamRoster.Items.Clear();
            try
            {
                foreach (Team team in prevTeams)
                {
                    foreach (Player player in team.Players)
                    {
                        if (item == player.Name)
                        {
                            Pages.ChangeStatsPage.number.Text = player.NumberOfPlayer.ToString();
                            Pages.ChangeStatsPage.position.Text = player.Position;
                            Pages.ChangeStatsPage.heightWeight.Text = player.Growth + ";" + player.Weight;
                            Pages.ChangeStatsPage.yearOfDraft.Text = player.YearOfDraft.ToString();
                            Pages.ChangeStatsPage.ptsRbsAst.Text = player.PPG + ";" + player.RPG + ";" + player.APG;
                            Pages.ChangeStatsPage.blkStlTo.Text = player.BPG + ";" + player.SPG + ";" + player.TPG;
                            Pages.ChangeStatsPage.fgFt3pt.Text = player.FGPercentage + ";" + player.FTPercentage + ";" + player.ThreeptPercentage;
                        }
                    }
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
