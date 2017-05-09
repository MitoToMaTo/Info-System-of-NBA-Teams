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

        private void showPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            playerStats.Items.Clear();
            item = rosterList.SelectedItem.ToString();
            foreach (Team team in wnd.teams)
            {
                foreach (Player player in team.Players)
                {
                    if(item == player.Name)
                    {
                        playerStats.Items.Add(player.PlayerInfo());
                    }
                }
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
            item = rosterList.SelectedItem.ToString();
            using (StreamWriter sw = new StreamWriter("../../Players.txt"))
            {
                foreach (Team team in wnd.teams)
                {
                    sw.WriteLine(team.ShortTeamInfoFile());
                    foreach (Player player in team.Players)
                    {
                        if (item != player.Name)
                        {
                            sw.WriteLine(player.PlayerInfoFile());
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void changePlayerStats_Click(object sender, RoutedEventArgs e)
        {
            ChangeStats changeStats = new ChangeStats(this);
            changeStats.Show();
            item = rosterList.SelectedItem.ToString();

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
    }
}
