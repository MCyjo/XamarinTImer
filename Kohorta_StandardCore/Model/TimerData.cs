using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;



namespace Kohorta_StandardCore.Model
{
    public static class TimerData
    {
        private static int timeRemained;
        public static int _timeRemained
        {
            get
            {
                return timeRemained;
            }
            set
            {
                timeRemained = value;
            }
        }

        private static int minutesRemained;
        public static int _minutesRemained
        {
            get
            {
                return minutesRemained;
            }
        }

        private static int secondsRemained;
        public static int _secondsRemained
        {
            get
            {
                return secondsRemained;
            }
        }

        private static int roundsRemained;
        public static int _roundRemained
        {
            get
            {
                return roundsRemained;
            }
            set
            {
                roundsRemained = value;
            }
        }

        private static bool activeRest=true;
        public static bool _activeRest
        { 
            get
            {
                return activeRest;
            }
        }

        private static System.Timers.Timer timer;
        public static Timer _timer { get => timer; set => timer = value; }

        public static void Tick(object sender, ElapsedEventArgs e)
        {
            if(timeRemained>0)
            {
                timeRemained--;

                CalculateTime();
            }
            else
            {
                timer.Enabled = false;
            }
            
            if(activeRest)
            {
                if((timeRemained % SettingsInfo._secondsOn) == 0)
                {
                    activeRest = false;
                }
            }
            else
            {
                if((timeRemained%SettingsInfo._secondsOff)==0)
                {
                    activeRest = true;
                }
            }


            if((timeRemained%(SettingsInfo._secondsOff + SettingsInfo._secondsOn))==0)
            {
                if(roundsRemained>0)
                {
                    roundsRemained--;
                }     
            }
        }

        public static void CalculateTime()
        {
            minutesRemained = timeRemained / 60;
            secondsRemained = timeRemained - (minutesRemained * 60);
        }

        public static string CreateClockString()
        {
            string minutes, seconds;
            if(minutesRemained<10)
            {
                minutes = "0" + minutesRemained.ToString();
            }
            else
            {
                minutes = minutesRemained.ToString();
            }
            if (secondsRemained < 10)
            {
                seconds = "0" + secondsRemained.ToString();
            }
            else
            {
                seconds = secondsRemained.ToString();
            }
            return (minutes + ":" + seconds);
        }

        public static void ResetTimer()
        {
            timeRemained = (SettingsInfo._secondsOff + SettingsInfo._secondsOn) * SettingsInfo._rounds;
            roundsRemained = SettingsInfo._rounds;
            activeRest = true;
        }
    }
}
