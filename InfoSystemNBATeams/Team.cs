using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoSystemNBATeams
{
    [Serializable]
    public class Team
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Название команды не должно быть пустым. ");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private string _nameOfCoach;
        public string NameOfCoach
        {
            get { return _nameOfCoach; }
            set
            {
                if ((String.IsNullOrWhiteSpace(value)) || (Regex.IsMatch(value, @"[0,0-9,9]")))
                {
                    throw new Exception("Имя тренера должно быть непустым и должно не содержать числа. ");
                }
                else
                {
                    _nameOfCoach = value;
                }
            }
        }

        private int _wins;
        public int Wins
        {
            get { return _wins; }
            set
            {
                if (!((value > 0) && (value < 82)))
                {
                    throw new Exception("Кол-во побед лежит в промежутке от 0 до 82 и в сумме с кол-вом поражений дает 82. ");
                }
                else
                {
                    _wins = value;
                }
            }
        }

        private int _loses;
        public int Loses
        {
            get { return _loses; }
            set
            {
                if (!((value > 0) && (value < 82) && (value == 82 - Wins)))
                {
                    throw new Exception("Кол-во поражений лежит в промежутке от 0 до 82 и в сумме с кол-вом побед дает 82. ");
                }
                else
                {
                    _loses = value;
                }
            }
        }

        private List<Player> _players; 
        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public Team()
        { }

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

        public string TeamInfoFile()
        {
            return string.Format("{0}\n{1}\n{2}-{3}\n\n{4}", Name, NameOfCoach, Wins, Loses, PlayersInfo());
        }
        
        public string ShortTeamInfoFile()
        {
            return string.Format("{0}\n{1}\n{2}-{3}\n", Name, NameOfCoach, Wins, Loses);
        }

        public string PlayersInfo()
        {
            string massive = "";
            foreach (Player player in Players)
            {
                string mass = player.PlayerInfoFile();
                massive = massive + mass;
            }
            return massive;
        }
    }
}
