using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для DeletePlayerPage.xaml
    /// </summary>
    public partial class DeletePlayerPage : Page
    {
        public DeletePlayerPage()
        {
            InitializeComponent();
        }

        string item;
        List<Team> prevTeams;
        XmlSerializer newFormatter;

        public string Method1(string sr)
        {
            item = sr;
            return item;
        }
        public List<Team> Method2(List<Team> tms)
        {
            prevTeams = tms;
            return prevTeams;
        }
        public XmlSerializer Method3(XmlSerializer xms)
        {
            newFormatter = xms;
            return newFormatter;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("../../Players.txt"))
            {
                if (File.Exists("../../Players.xml"))
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter("../../Players.txt"))
                        {
                            foreach (Team team in prevTeams)
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
                        foreach (Team team in prevTeams)
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
                        using (FileStream fs = new FileStream("../../Players.xml", FileMode.Create))
                        {
                            newFormatter.Serialize(fs, prevTeams);
                        }
                        Pages.RosterPage.rosterList.Items.Clear();
                        NavigationService.Navigate(Pages.RosterPage);
                        Pages.RosterPage.updateTeamRoster();
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
            else
            {
                MessageBox.Show(" Указан несуществующий путь. ");
            }
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.RosterPage);
        }
    }
}
