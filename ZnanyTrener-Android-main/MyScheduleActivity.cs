using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Schedule;
using Com.Syncfusion.Schedule.Enums;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Presenters;

namespace ZnanyTrener_Android
{
    [Activity(Label = "MyScheduleActivity")]
    public class MyScheduleActivity : AppCompatActivity
    {
        private SfSchedule schedule;
        private IMySchedulePresenter _presenter;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            schedule = new SfSchedule(this);
            SetContentView(schedule);
            schedule.ScheduleView = ScheduleView.WeekView;
            _presenter = new MySchedulePresenter(this);

            schedule.ItemsSource = await _presenter.GetAppointmentsAsync();
            schedule.CellTapped += Schedule_CellTapped;
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {            
            var appointment = e.ScheduleAppointment;
            var request = _presenter.GetTrainingToAddRequest(appointment);
            var trans = SupportFragmentManager.BeginTransaction();
            var fragment = new PaymentFragment(request, _presenter.CanAddTraining)
            {
                Cancelable = true
            };
            fragment.Show(trans, "tapped_cell");
        }
    }
}