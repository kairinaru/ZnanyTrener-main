using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Schedule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZnanyTrener_Android.ApiConnections.Services;
using ZnanyTrener_Android.Helpers;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public class MySchedulePresenter : IMySchedulePresenter
    {
        private readonly MyScheduleActivity _activity;
        private readonly ITrainingService _trainingService;
        private readonly string _fromWhom;

        public MySchedulePresenter(MyScheduleActivity activity)
        {
            _activity = activity;
            _trainingService = new TrainingService();
            _fromWhom = _activity.Intent?.GetStringExtra("for");
        }

        public List<TrainingResponse> Trainings { get; private set; }
        public bool CanAddTraining
        {
            get
            {
                return _fromWhom != "me-user" && _fromWhom != "me-coach";
            }
        }

        public async Task<ScheduleAppointmentCollection> GetAppointmentsAsync()
        {
            ScheduleAppointmentCollection appointments = new ScheduleAppointmentCollection();
            try
            {
                string response;
                var userId = SharedPreferencesManager.GetUser().Id;

                if (_fromWhom == "me-coach")
                {
                    response = await _trainingService.GetCoachAppointments(userId);
                }
                else if(_fromWhom == "me-user")
                {
                    response = await _trainingService.GetUserAppointments(userId);
                }
                else
                {
                    response = await _trainingService.GetCoachAppointments(int.Parse(_fromWhom));
                }
                                
                if (response != null)
                {
                    Trainings = JsonConvert.DeserializeObject<List<TrainingResponse>>(response);

                    if (CanAddTraining)
                    {
                        appointments = CalendarHelper.GetFree(Trainings);
                    }
                    else
                    {
                        appointments = CalendarHelper.GetAppointments(Trainings);
                    }                    
                }
            }
            catch (Exception exception)
            {
                Toast.MakeText(_activity, exception.Message, ToastLength.Short).Show();
            }

            return appointments;
        }

        public TrainingToAddRequest GetTrainingToAddRequest(ScheduleAppointment appointment)
        {
            var splittedString = appointment.Notes.Split('/');
            var startYear = int.Parse(splittedString[0]);
            var startMonth = int.Parse(splittedString[1]);
            var startDay = int.Parse(splittedString[2]);
            var startHour = int.Parse(splittedString[3]);
            var startMinute = int.Parse(splittedString[4]);
            var endHour = int.Parse(splittedString[5]);
            var endMinute = int.Parse(splittedString[6]);

            var startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
            var endDate = new DateTime(startYear, startMonth, startDay, endHour, endMinute, 0);

            TrainingToAddRequest trainingToAdd = new TrainingToAddRequest
            {
                StartDate = startDate,
                EndDate = endDate,
            };

            if (CanAddTraining)
            {
                trainingToAdd.UserId = SharedPreferencesManager.GetUser().Id;
                trainingToAdd.CoachId = int.Parse(_fromWhom);                
            }

            return trainingToAdd;
        }
    }
}