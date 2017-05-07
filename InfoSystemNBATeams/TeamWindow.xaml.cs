﻿using System;
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
    /// Логика взаимодействия для TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        List<Player> players = new List<Player>();
        List<Team> teams = new List<Team>();
        public string item;

        public TeamWindow()
        {
            InitializeComponent();
        }

        private void readTXT_Click(object sender, RoutedEventArgs e)
        {
            teamList.Items.Clear();
            teams.Clear();
            players.Clear();
            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
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

                        players.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to));
                        if (sr.EndOfStream)
                            break;
                    }
                    teams.Add(new Team(teamName, coachName, wins, loses, players));
                }
            }
            foreach (Team team in teams)
            {
                teamList.Items.Add(team.Name);
            }
        }

        private void showTeamInfo_Click(object sender, RoutedEventArgs e)
        {
            teamRoster.Items.Clear();
            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                item = teamList.SelectedItem.ToString();
                while (!sr.EndOfStream)
                {
                    players.Clear();
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
                        players.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to));
                        if (sr.EndOfStream)
                            break;
                    }
                    Team team = new Team(teamName, coachName, wins, loses, players);
                    if (item.ToString() == teamName)
                    {
                        teamRoster.Items.Add(team.TeamInfo());
                    }
                }
            }
        }

        private void showTeamRoster_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader("../../Players.txt", Encoding.Default))
            {
                item = teamList.SelectedItem.ToString();
                while (!sr.EndOfStream)
                {
                    players.Clear();
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

                        players.Add(new Player(playerName, num, position, growth, weight, yearOfDraft, pts, rbs, ast, stl, blk, fg, ft, tp, to));
                        if (sr.EndOfStream)
                            break;
                    }
                    teams.Add(new Team(teamName, coachName, wins, loses, players));

                    if (item.ToString() == teamName)
                    {
                        MainWindow mainwindow = new MainWindow();
                        mainwindow.Show();
                        foreach (Player player in players)
                        {
                            mainwindow.rosterList.Items.Add(player.Name);
                        }
                    }
                }
            }
        }

        private void changeTeamInfo_Click(object sender, RoutedEventArgs e)
        {
            item = teamList.SelectedItem.ToString();

            ChangeTeamInfo changeTeamInfo = new ChangeTeamInfo(this);
            changeTeamInfo.Show();

            using (StreamReader sr = new StreamReader("../../Players.txt"))
            {
                while(!sr.EndOfStream)
                {
                    if(item == sr.ReadLine())
                    {
                        changeTeamInfo.teamName.Text = item;
                        changeTeamInfo.coachName.Text = sr.ReadLine();
                        string[] mass = sr.ReadLine().Split('-');
                        changeTeamInfo.numWins.Text = mass[0];
                        changeTeamInfo.numLoses.Text = mass[1];
                    }
                }
            }
        }
    }
}
