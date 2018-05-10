using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Kohorta_StandardCore.Model;

namespace KohrtaTimer
{
    [Activity(Label = "Timer")]
    public class TimerActivity : Activity
    {
        Thread muzaThread;

        MediaPlayer player;
        Button StartPauseBtn;
        Button StopBtn;
        Button BackBtn;
        TextView mainTextView;
        TextView secondsElapsedTextView;
        TextView roundsRemainedTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TimerLayout);

            FindViews();
            HandleEvents();

            

            TimerData.ResetTimer();
            TimerData.CalculateTime();
            UpdateClock();
            secondsElapsedTextView.SetBackgroundColor(Android.Graphics.Color.ForestGreen);


            TimerData._timer = new System.Timers.Timer();
            TimerData._timer.Interval = 1000;
            TimerData._timer.Elapsed += TimerData.Tick;
            TimerData._timer.Elapsed += UpdateClock;
            TimerData._timer.Elapsed += CheckOnRest;
            
        }

        private void FindViews()
        {
            StartPauseBtn = FindViewById<Button>(Resource.Id.startTimerButton);
            StopBtn = FindViewById<Button>(Resource.Id.stopTimerButton);
            BackBtn = FindViewById<Button>(Resource.Id.backButton);
            mainTextView = FindViewById<TextView>(Resource.Id.mainTextView);
            secondsElapsedTextView = FindViewById<TextView>(Resource.Id.timeElapsedTextView);
            roundsRemainedTextView = FindViewById<TextView>(Resource.Id.roundsElapsedTextView);
        }

        private void HandleEvents()
        {
            StartPauseBtn.Click += startTicking;
            StopBtn.Click += stopTicking;
            BackBtn.Click += backToMenu;
        }


        private void backToMenu(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }


        public void CheckOnRest(object sender, EventArgs e)
        {
            CheckOnRest();
        }



        public void CheckOnRest()
        {
            
            if (TimerData._activeRest)
            {   
                if(mainTextView.Text!="Go!")
                {
                    muzaThread = new Thread(new ParameterizedThreadStart(PlaySound));
                    muzaThread.Start(Resource.Raw.GOGOGO);

                    secondsElapsedTextView.SetBackgroundColor(Android.Graphics.Color.ForestGreen);
                    mainTextView.Text = "Go!";
                   
                //    PlaySound(Resource.Raw.GOGOGO);        //muza        
                }
                
            }
            else
            {
                if(mainTextView.Text!="Stop!")
                {
                    muzaThread = new Thread(new ParameterizedThreadStart(PlaySound));
                    muzaThread.Start(Resource.Raw.STOP);

                    secondsElapsedTextView.SetBackgroundColor(Android.Graphics.Color.Red);
                    mainTextView.Text = "Stop!";
                    
                  //  PlaySound(Resource.Raw.STOP);        //muza
                    
                }
                
            }
        }

        public void UpdateClock(object sender, EventArgs e)
        {
            UpdateClock();
        }

        
        public void UpdateClock()
        {
            RunOnUiThread(() => secondsElapsedTextView.Text = TimerData.CreateClockString());
            RunOnUiThread(() => roundsRemainedTextView.Text = TimerData._roundRemained.ToString()+"/"+SettingsInfo._rounds.ToString());
        }

        private void stopTicking(object sender, EventArgs e)
        {
            TimerData._timer.Stop();
            TimerData._timer.Enabled = false;

            StartPauseBtn.Text = "START";
            StartPauseBtn.Click += startTicking;
            StartPauseBtn.Click -= pauseTicking;

           
            TimerData.ResetTimer();
            UpdateClock();
        }

        private void startTicking(object sender, EventArgs e)
        {
            StartPauseBtn.Text = "PAUSE";
            StartPauseBtn.Click -= startTicking;
            StartPauseBtn.Click += pauseTicking;

            TimerData._timer.Enabled = true;
            TimerData._timer.Start();
        }

        private void pauseTicking(object sender, EventArgs e)
        {
            TimerData._timer.Stop();
            TimerData._timer.Enabled = false;

            StartPauseBtn.Text = "START";
            StartPauseBtn.Click += startTicking;
            StartPauseBtn.Click -= pauseTicking;
        }

        private  void PlaySound(object id)
        {
            player = MediaPlayer.Create(this, Int32.Parse(id.ToString()));
            player.Start();
            if( player.IsPlaying==false)
            {
                muzaThread.Abort();
            }
        }
    }
}