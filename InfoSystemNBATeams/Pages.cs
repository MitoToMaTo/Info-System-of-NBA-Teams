using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InfoSystemNBATeams
{
    class Pages
    {
        private static TeamPage _teamPage = new TeamPage();
        private static RosterPage _rosterPage = new RosterPage();
        private static ChangeTeamInfoPage _changeTeamInfoPage = new ChangeTeamInfoPage();
        private static ChangeStatsPage _changeStatsPage = new ChangeStatsPage();
        private static DeletePlayerPage _deletePlayerPage = new DeletePlayerPage();
        private static CreatePlayerPage _createPlayerPage = new CreatePlayerPage();

        public static TeamPage TeamPage
        {
            get
            {
                return _teamPage;
            }
        }
        public static RosterPage RosterPage
        {
            get
            {
                return _rosterPage;
            }
        }
        public static ChangeTeamInfoPage ChangeTeamInfoPage
        {
            get
            {
                return _changeTeamInfoPage;
            }
        }
        public static ChangeStatsPage ChangeStatsPage
        {
            get
            {
                return _changeStatsPage;
            }
        }
        public static DeletePlayerPage DeletePlayerPage
        {
            get
            {
                return _deletePlayerPage;
            }
        }
        public static CreatePlayerPage CreatePlayerPage
        {
            get
            {
                return _createPlayerPage;
            }
        }

        public static string FirstUpper(string str)
        {
            string[] s = str.Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                {
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1).ToLower();
                }
                else
                    s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }
    }
}
