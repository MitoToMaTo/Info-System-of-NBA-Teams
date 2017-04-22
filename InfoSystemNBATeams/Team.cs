using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystemNBATeams
{
    class Team
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _nameOfCoach;
        public string NameOfCoach
        {
            get { return _nameOfCoach; }
            set { _nameOfCoach = value; }
        }

        private int _wins;
        public int Wins
        {
            get { return _wins; }
            set { _wins = value; }
        }

        private int _loses;
        public int Loses
        {
            get { return _loses; }
            set { _loses = value; }
        }

        private List<Player> _players; 
        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public Team (string name, string nameOfCoach, int wins, int loses, List<Player> players)
        {
            Name = name;
            NameOfCoach = nameOfCoach;
            Wins = wins;
            Loses = loses;
            Players = players;
        }

        public string TeamInfo()
        {
            return string.Format("Команда: {0}\nТренер: {1}\nПобед-поражений: {2}-{3}\nСостав: {4}", Name, NameOfCoach, Wins, Loses, PlayersNames());
        }

        public string PlayersNames()
        {
            string name = "\n";
            foreach (Player player in Players)
            {
                name = name + player.Name + "\n";
            }
            return name;
        }
    }
}
