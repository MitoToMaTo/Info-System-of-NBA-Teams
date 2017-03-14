﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystemNBATeams
{
    class Player
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _numberOfPlayer;
        public int NumberOfPlayer
        {
            get { return _numberOfPlayer; }
            set { _numberOfPlayer = value; }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private double _growth;
        public double Growth
        {
            get { return _growth; }
            set { _growth = value; }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private int _yearOfDraft;
        public int YearOfDraft
        {
            get { return _yearOfDraft; }
            set { _yearOfDraft = value; }
        }

        private double _ppg;
        public double PPG
        {
            get { return _ppg; }
            set { _ppg = value; }
        }

        private double _rpg;
        public double RPG
        {
            get { return _rpg; }
            set { _rpg = value; }
        }

        private double _apg;
        public double APG
        {
            get { return _apg; }
            set { _apg = value; }
        }

        private double _spg;
        public double SPG
        {
            get { return _spg; }
            set { _spg = value; }
        }

        private double _bpg;
        public double BPG
        {
            get { return _bpg; }
            set { _bpg = value; }
        }

        private double _fgPercentage;
        public double FGPercentage
        {
            get { return _fgPercentage; }
            set { _fgPercentage = value; }
        }

        private double _ftPercentage;
        public double FTPercentage
        {
            get { return _ftPercentage; }
            set { _ftPercentage = value; }
        }

        private double _threeptPercentage;
        public double ThreeptPercentage
        {
            get { return _threeptPercentage; }
            set { _threeptPercentage = value; }
        }

        private double _tpg;
        public double TPG
        {
            get { return _tpg; }
            set { _tpg = value; }
        }

        public Player(string name, int numberOfPlayer, string position, double growth, double weight, int yearOfDraft, double ppg, double rpg, double apg, double spg, double bpg, double fgPercentage, double ftPercentage, double threeptPercentage, double tpg)
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
            return string.Format(" Имя игрока: {0} \n Номер игрока: {1} \n Позиция: {2} \n Рост: {3}m \n Вес: {4}kg \n Год драфта: {5} ", Name, NumberOfPlayer, Position, Growth, Weight, YearOfDraft);
        }
        
        public string PlayerStats()
        {
            return string.Format(" Очков в среднем за игру: {0} \n Подборов в среднем за игру: {1} \n Передач в среднем за игру: {2} \n Перехватов в среднем за игру: {3} \n Блоков в среднем за игру: {4} \n Потерь в среднем за игру: {5} \n Процент попаданий с игры: {6}% \n Процент попаданий штрафных: {7}% \n Процент попаданий трехочковых: {8}% ", PPG, RPG, APG, SPG, BPG, TPG, FGPercentage, FTPercentage, ThreeptPercentage);
        }
    }
}