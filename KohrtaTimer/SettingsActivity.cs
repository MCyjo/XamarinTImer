using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kohorta_StandardCore.Model;
using KohrtaTimer;



namespace KohrtaTimer
{
    [Activity(Label = "Settings", MainLauncher = true)]
    public class SettingsActivity : Activity
    {
        private Button decreaseOnTimeBtn;
        private Button decreaseOffTimeBtn;
        private Button increaseOnTimeBtn;
        private Button increaseOffTimeBtn;
        private Button increaseRoundsBtn;
        private Button decreaseRoundsBtn;
        private Button startTimerBtn;
        private EditText timeOnEditText;
        private EditText timeOffEditText;
        private TextView roundsTextView; 
        private TextView totalTimeTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SettingsInfo.ResetData();

            SetContentView(Resource.Layout.SettingsLayout);
            FindViews();
            HandleEvents();
            totalTimeTextView.Text =  Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void FindViews()
        {
            decreaseOffTimeBtn = FindViewById<Button>(Resource.Id.decreaseTimeOffButton);
            increaseOffTimeBtn = FindViewById<Button>(Resource.Id.increaseTimeOffButton);
            decreaseOnTimeBtn = FindViewById<Button>(Resource.Id.decreaseTimeOnButton);
            increaseOnTimeBtn = FindViewById<Button>(Resource.Id.increaseTimeOnButton);
            increaseRoundsBtn = FindViewById<Button>(Resource.Id.increaseRoundsButton);
            decreaseRoundsBtn = FindViewById<Button>(Resource.Id.decreaseRoundsButton);
            startTimerBtn = FindViewById<Button>(Resource.Id.startButton);
            timeOnEditText = FindViewById<EditText>(Resource.Id.timeOnEditText);
            timeOffEditText = FindViewById<EditText>(Resource.Id.timeOffEditText);
            roundsTextView = FindViewById<TextView>(Resource.Id.roundsTextView);
            totalTimeTextView = FindViewById<TextView>(Resource.Id.totalTimeTextView);
        }

        private void HandleEvents()
        {
            increaseOnTimeBtn.Click += IncreaseOnTime_Click;
            decreaseOnTimeBtn.Click += DecreaseOnTime_Click;
            increaseOffTimeBtn.Click += IncreaseOffTime_Click;
            decreaseOffTimeBtn.Click += DecreaseOffTime_Click;
            increaseRoundsBtn.Click += IncreaseRounds_Click;
            decreaseRoundsBtn.Click += DecreaseRounds_Click;
            startTimerBtn.Click += StartTimer_Click;
        }

        private void StartTimer_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(TimerActivity));
            StartActivity(intent);
        }

        private void DecreaseRounds_Click(object sender, EventArgs e)
        {
            SettingsInfo._rounds--;
            roundsTextView.Text = SettingsInfo._rounds.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void IncreaseRounds_Click(object sender, EventArgs e)
        {
            SettingsInfo._rounds++;
            roundsTextView.Text = SettingsInfo._rounds.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void DecreaseOffTime_Click(object sender, EventArgs e)
        {
            SettingsInfo._secondsOff--;
            timeOffEditText.Text = SettingsInfo._secondsOff.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void IncreaseOffTime_Click(object sender, EventArgs e)
        {
            SettingsInfo._secondsOff++;
            timeOffEditText.Text = SettingsInfo._secondsOff.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void DecreaseOnTime_Click(object sender, EventArgs e)
        {
            SettingsInfo._secondsOn--;
            timeOnEditText.Text = SettingsInfo._secondsOn.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }

        private void IncreaseOnTime_Click(object sender, EventArgs e)
        {
            SettingsInfo._secondsOn++;
            timeOnEditText.Text = SettingsInfo._secondsOn.ToString();
            totalTimeTextView.Text = Resources.GetString(Resource.String.total_time) + SettingsInfo.UpdateTotalTime().ToString();
        }
    }
}