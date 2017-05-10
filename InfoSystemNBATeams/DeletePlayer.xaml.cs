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
    /// Логика взаимодействия для DeletePlayer.xaml
    /// </summary>
    public partial class DeletePlayer : Window
    {
        MainWindow wnd;
        public DeletePlayer(MainWindow w)
        {
            InitializeComponent();
            wnd = w;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            string item = wnd.rosterList.SelectedItem.ToString();

            if (File.Exists("../../Players.txt"))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter("../../Players.txt"))
                    {
                        foreach (Team team in wnd.prevTeams)
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
                    foreach (Team team in wnd.prevTeams)
                    {
                        foreach (Player player in team.Players)
                        {
                            if (item == player.Name)
                            {
                                team.Players.Remove(player);
                                break;
                            }
                        }
                    }
                    Close();
                    wnd.rosterList.Items.Clear();
                    wnd.updateTeamRoster();
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

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
