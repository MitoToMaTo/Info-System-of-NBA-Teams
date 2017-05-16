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

// СВИСТОПЛЯСКА С МАССИВАМИ players; разобраться с ними

namespace InfoSystemNBATeams
{
    /// <summary>
    /// Логика взаимодействия для editStats.xaml
    /// </summary>
    public partial class EditStats : Window
    {
        MainWindow wnd;
        public EditStats(MainWindow w)
        {
            InitializeComponent();
            wnd = w;
        }

        private void createNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((String.IsNullOrWhiteSpace(name.Text)) || (String.IsNullOrWhiteSpace(number.Text)) || (String.IsNullOrWhiteSpace(position.Text)) || (String.IsNullOrWhiteSpace(heightWeight.Text)) || (String.IsNullOrWhiteSpace(yearOfDraft.Text)) || (String.IsNullOrWhiteSpace(ptsRbsAst.Text)) || (String.IsNullOrWhiteSpace(blkStlTo.Text)) || (String.IsNullOrWhiteSpace(fgFt3pt.Text)) || (String.IsNullOrWhiteSpace(teamName.Text)))
                {
                    MessageBox.Show(" Все поля должны быть непустыми. ");
                    return;
                }

                string nameOfPlayer = name.Text;
                int numOfPlayer = int.Parse(number.Text);
                string pos = position.Text;
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

                string nameOfTeam = teamName.Text;

                bool isFound = false;
                foreach (Team team in wnd.prevTeams)
                {
                    if (team.Name == nameOfTeam)
                    {
                        isFound = true;
                        break; 
                    }
                }
                if(!isFound)
                {
                    MessageBox.Show(" Команды с таким названием нет. ");
                    return;
                }

                Player newPlayer = new Player(nameOfPlayer, numOfPlayer, pos, playerGrowth, playerWeight, draftYear, points, rebounds, assists, steals, blocks, fgPercentage, ftPercentage, threePtPercentage, turnovers);

                if (File.Exists("../../Players.txt"))
                {
                    using (StreamWriter sw = new StreamWriter("../../Players.txt"))
                    {
                        foreach (Team team in wnd.prevTeams)
                        {
                            sw.WriteLine(team.ShortTeamInfoFile());
                            if (nameOfTeam == team.Name)
                            {
                                team.Players.Add(newPlayer);
                            }
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
                    using (FileStream fs = new FileStream("../../Players.xml", FileMode.Open))
                    {
                        wnd.newFormatter.Serialize(fs, wnd.prevTeams);
                    }
                    Close();
                    wnd.rosterList.Items.Clear();
                    wnd.updateTeamRoster();
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
    }
}
