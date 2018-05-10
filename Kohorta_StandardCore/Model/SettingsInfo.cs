using System;
using System.Collections.Generic;
using System.Text;

namespace Kohorta_StandardCore.Model
{
    public static class SettingsInfo
    {
        private static int secondsOn=1;
        public static int _secondsOn
        {
            get
            {
                return secondsOn;
            }
            set
            {
                if(value>0)
                {
                    secondsOn = value;
                }
                
            }
        }


        private static int secondsOff=1;
        public static int _secondsOff
        {
            get
            {
                return secondsOff;
            }
            set
            {   
                if(value>0)
                {
                    secondsOff = value;
                }
            }
        }

        private static int rounds=1;
        public static int _rounds
        {
            get
            {
                return rounds;
            }
            set
            {
                if(value>0)
                {
                    rounds = value;
                }
            }
        }

        public static void ResetData()
        {
            secondsOff = 1;
            secondsOn = 1;
            rounds = 1;
        }

        private static int totalTime;
        public static int _totalTime
        {
            get
            {
                return totalTime;
            }
            set
            {
                totalTime = value;
            }
        }

        public static int UpdateTotalTime()
        {
            return ((secondsOn + secondsOff) * rounds);
        }
    }
}
