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
        public string item;
        TeamWindow wnd;
        public List<Team> prevTeams;

        public MainWindow(TeamWindow w)
        {
            InitializeComponent();
            wnd = w;
            prevTeams = wnd.teams;
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

            if (File.Exists("../../Players.txt"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            if (sr.ReadLine() == item)
                            {
                                string[] orgInfo = sr.ReadLine().Split(',');
                                changeStats.number.Text = orgInfo[0];
                                changeStats.position.Text = orgInfo[1];
                                changeStats.heightWeight.Text = orgInfo[2] + ";" + orgInfo[3];
                                changeStats.yearOfDraft.Text = sr.ReadLine();
                                string[] stats = sr.ReadLine().Split(' ');
                                changeStats.ptsRbsAst.Text = stats[0] + ";" + stats[1] + ";" + stats[2];
                                changeStats.blkStlTo.Text = stats[3] + ";" + stats[4] + ";" + stats[5];
                                changeStats.fgFt3pt.Text = stats[6] + ";" + stats[7] + ";" + stats[8];
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
    }
}
