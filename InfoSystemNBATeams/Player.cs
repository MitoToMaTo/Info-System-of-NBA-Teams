using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoSystemNBATeams
{
    public class Player
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if ((String.IsNullOrWhiteSpace(value)) || (Regex.IsMatch(value, @"[0,0-9,9]")))
                {
                    throw new Exception("Имя игрока должно быть непустым и должно не содержать числа. ");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private int _numberOfPlayer;
        public int NumberOfPlayer
        {
            get { return _numberOfPlayer; }
            set
            {
                if (!((value >= 0) && (value <= 99)))
                {
                    throw new Exception("Номер игрока: от 0 до 99. ");
                }
                else
                {
                    _numberOfPlayer = value;
                }
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (!((value == "PG") || (value == "SG") || (value == "SF") || (value == "PF") || (value == "C")))
                {
                    throw new Exception("Позиция игрока: PG, SG, SF, PF или C. ");
                }
                else
                {
                    _position = value;
                }
            }
        }

        private int _growth;
        public int Growth
        {
            get { return _growth; }
            set
            {
                if (!(value > 0))
                {
                    throw new Exception("Рост - целое положительное число (в сантиметрах). ");
                }
                else
                {
                    _growth = value;
                }
            }
        }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (!(value > 0))
                {
                    throw new Exception("Вес - целое положительное число (в килограммах). ");
                }
                else
                {
                    _weight = value;
                }
            }
        }

        private int _yearOfDraft;
        public int YearOfDraft
        {
            get { return _yearOfDraft; }
            set
            {
                if (!((value > 1950) && (value < 2017)))
                {
                    throw new Exception("Драфт не мог быть раньше 1950 года или позже 2017. ");
                }
                _yearOfDraft = value;
            }
        }

        private double _ppg;
        public double PPG
        {
            get { return _ppg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во очков - неотрицательное число. ");
                }
                else
                {
                    _ppg = value;
                }
            }
        }

        private double _rpg;
        public double RPG
        {
            get { return _rpg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во подборов - неотрицательное число. ");
                }
                else
                {
                    _rpg = value;
                }
            }
        }

        private double _apg;
        public double APG
        {
            get { return _apg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во ассистов - неотрицательное число. ");
                }
                else
                {
                    _apg = value;
                }
            }
        }

        private double _spg;
        public double SPG
        {
            get { return _spg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во перехватов - неотрицательное число. ");
                }
                else
                {
                    _spg = value;
                }
            }
        }

        private double _bpg;
        public double BPG
        {
            get { return _bpg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во блоков - неотрицательное число. ");
                }
                else
                {
                    _bpg = value;
                }
            }
        }

        private double _fgPercentage;
        public double FGPercentage
        {
            get { return _fgPercentage; }
            set
            {
                if (!((value >= 0.0) && (value <= 99.9)))
                {
                    throw new Exception("Процент попаданий - число от 0.0 до 99.9. ");
                }
                else
                {
                    _fgPercentage = value;
                }
            }
        }

        private double _ftPercentage;
        public double FTPercentage
        {
            get { return _ftPercentage; }
            set
            {
                if (!((value >= 0.0) && (value <= 99.9)))
                {
                    throw new Exception("Процент попаданий - число от 0.0 до 99.9. ");
                }
                else
                {
                    _ftPercentage = value;
                }
            }
        }

        private double _threeptPercentage;
        public double ThreeptPercentage
        {
            get { return _threeptPercentage; }
            set
            {
                if (!((value >= 0.0) && (value <= 99.9)))
                {
                    throw new Exception("Процент попаданий - число от 0.0 до 99.9. ");
                }
                else
                {
                    _threeptPercentage = value;
                }
            }
        }

        private double _tpg;
        public double TPG
        {
            get { return _tpg; }
            set
            {
                if (!(value >= 0.0))
                {
                    throw new Exception("Кол-во потерь - неотрицательное число. ");
                }
                else
                {
                    _tpg = value;
                }
            }
        }

        public Player(string name, int numberOfPlayer, string position, int growth, int weight, int yearOfDraft, double ppg, double rpg, double apg, double spg, double bpg, double fgPercentage, double ftPercentage, double threeptPercentage, double tpg)
        {
            Name = name;
            NumberOfPlayer = numberOfPlayer;
            Position = position;
            Growth = growth;
            Weight = weight;
            YearOfDraft = yearOfDraft;
            PPG = ppg;
            RPG = rpg;
            APG = apg;
            SPG = spg;
            BPG = bpg;
            TPG = tpg;
            FGPercentage = fgPercentage;
            FTPercentage = ftPercentage;
            ThreeptPercentage = threeptPercentage;
        }

        public string PlayerInfo()
        {
            return string.Format("Имя игрока: {0} \nНомер игрока: {1} \nПозиция: {2} \nРост: {3}cm \nВес: {4}kg \nГод драфта: {5} \nОчков в среднем за игру: {6} \nПодборов в среднем за игру: {7} \nПередач в среднем за игру: {8} \nПерехватов в среднем за игру: {9}\nБлоков в среднем за игру: {10} \nПотерь в среднем за игру: {11} \nПроцент попаданий с игры: {12}% \nПроцент попаданий штрафных: {13}% \nПроцент попаданий трехочковых: {14}% ", Name, NumberOfPlayer, Position, Growth, Weight, YearOfDraft, PPG, RPG, APG, SPG, BPG, TPG, FGPercentage, FTPercentage, ThreeptPercentage);
        }

        public string PlayerInfoFile()
        {
            return string.Format("{0}\n{1},{2},{3},{4}\n{5}\n{6} {7} {8} {9} {10} {11} {12} {13} {14}", Name, NumberOfPlayer, Position, Growth, Weight, YearOfDraft, PPG, RPG, APG, SPG, BPG, TPG, FGPercentage, FTPercentage, ThreeptPercentage);
        }
    }
}
