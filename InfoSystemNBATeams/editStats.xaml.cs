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
    /// Логика взаимодействия для editStats.xaml
    /// </summary>
    public partial class editStats : Window
    {
        public editStats()
        {
            InitializeComponent();
        }

        private void createNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("../../Player.txt"))
            {

            }
        }
    }
}
