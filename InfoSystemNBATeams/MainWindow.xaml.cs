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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string item;
        TeamWindow wnd;
        public List<Team> prevTeams;
        public XmlSerializer newFormatter;

        public MainWindow(TeamWindow w)
        {
            InitializeComponent();
            wnd = w;
            prevTeams = wnd.teams;
            newFormatter = wnd.formatter;
        }

        public void updateTeamRoster()
        {
            string newItem = wnd.teamList.SelectedItem.ToString();
            foreach (Team t in wnd.teams)
            {
                if (newItem == t.Name)
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
                foreach (Team team in wnd.teams)
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
            EditStats editStats = new EditStats(this);
            editStats.teamName.Text = wnd.item;
            editStats.Show();
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
            }

            DeletePlayer deletePlayer = new DeletePlayer(this);
            deletePlayer.Show();
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
            }

            ChangeStats changeStats = new ChangeStats(this);
            changeStats.Show();

            try
            {
                foreach (Team team in wnd.teams)
                {
                    foreach (Player player in team.Players)
                    {
                        if(item == player.Name)
                        {
                            changeStats.number.Text = player.NumberOfPlayer.ToString();
                            changeStats.position.Text = player.Position;
                            changeStats.heightWeight.Text = player.Growth + ";" + player.Weight;
                            changeStats.yearOfDraft.Text = player.YearOfDraft.ToString();
                            changeStats.ptsRbsAst.Text = player.PPG + ";" + player.RPG + ";" + player.APG;
                            changeStats.blkStlTo.Text = player.BPG + ";" + player.SPG + ";" + player.TPG;
                            changeStats.fgFt3pt.Text = player.FGPercentage + ";" + player.FTPercentage + ";" + player.ThreeptPercentage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
