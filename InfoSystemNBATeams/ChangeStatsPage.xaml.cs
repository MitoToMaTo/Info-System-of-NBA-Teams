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
    /// Логика взаимодействия для ChangeStatsPage.xaml
    /// </summary>
    public partial class ChangeStatsPage : Page
    {
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

        public ChangeStatsPage()
        {
            InitializeComponent();
        }

        private void changePlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((String.IsNullOrWhiteSpace(number.Text)) || (String.IsNullOrWhiteSpace(position.Text)) || (String.IsNullOrWhiteSpace(heightWeight.Text)) || (String.IsNullOrWhiteSpace(yearOfDraft.Text)) || (String.IsNullOrWhiteSpace(ptsRbsAst.Text)) || (String.IsNullOrWhiteSpace(blkStlTo.Text)) || (String.IsNullOrWhiteSpace(fgFt3pt.Text)))
                {
                    MessageBox.Show(" Все поля должны быть непустыми. ");
                    return;
                }

                string name = item;

                int numOfPlayer = int.Parse(number.Text);
                string pos = position.Text.ToUpper();
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

                if (File.Exists("../../Players.txt"))
                {
                    using (StreamWriter sw = new StreamWriter("../../Players.txt"))
                    {
                        foreach (Team team in prevTeams)
                        {
                            sw.WriteLine(team.ShortTeamInfoFile());
                            foreach (Player player in team.Players)
                            {
                                if (name == player.Name)
                                {
                                    player.NumberOfPlayer = numOfPlayer;
                                    player.Position = pos;
                                    player.Growth = playerGrowth;
                                    player.Weight = playerWeight;
                                    player.YearOfDraft = draftYear;
                                    player.PPG = points;
                                    player.RPG = rebounds;
                                    player.APG = assists;
                                    player.SPG = steals;
                                    player.BPG = blocks;
                                    player.TPG = turnovers;
                                    player.FGPercentage = fgPercentage;
                                    player.FTPercentage = ftPercentage;
                                    player.ThreeptPercentage = threePtPercentage;
                                }
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
                    using (FileStream fs = new FileStream("../../Players.xml", FileMode.Open))
                    {
                        newFormatter.Serialize(fs, prevTeams);
                    }
                    Pages.RosterPage.playerStats.Items.Clear();
                    NavigationService.Navigate(Pages.RosterPage);
                }
                else
                {
                    MessageBox.Show(" Указан несуществующий путь. ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.RosterPage);
        }
    }
}
