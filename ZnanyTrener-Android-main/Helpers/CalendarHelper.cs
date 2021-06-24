using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Schedule;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Helpers
{
    public static class CalendarHelper
    {       
        public static ScheduleAppointmentCollection GetFree(IEnumerable<TrainingResponse> planned)
        {
            var all = LoopMonths();

            RemovePlanned(all, planned);

            return all;
        }

        public static ScheduleAppointmentCollection GetAppointments(IEnumerable<TrainingResponse> planned)
        {
            ScheduleAppointmentCollection appointments = new ScheduleAppointmentCollection();

            foreach (var date in planned)
            {
                var startDate = date.StartDate;
                var endDate = date.EndDate;

                var appointment = CreateAppointmentFromDateTime(startDate.Year, startDate.Month-1, startDate.Day,
                    startDate.Hour - 1, startDate.Minute, endDate.Hour - 1, endDate.Minute);

                appointments.Add(appointment);
            }

            return appointments;
        }

        private static void RemovePlanned(ScheduleAppointmentCollection free, IEnumerable<TrainingResponse> planned)
        {
            foreach (var date in planned)
            {
                var startDate = date.StartDate;
                var endDate = date.EndDate;

                var appointment = CreateAppointmentFromDateTime(startDate.Year, startDate.Month, startDate.Day,
                    startDate.Hour - 1, startDate.Minute, endDate.Hour - 1, endDate.Minute);

                var appointmentToRemove = free.FirstOrDefault(x => x.Notes == appointment.Notes);
                free.Remove(appointmentToRemove);
            }
        }

        private static ScheduleAppointmentCollection LoopMonths()
        {
            var meetings = new ScheduleAppointmentCollection();

            for (int i = 1; i <= 12; i++)
            {
                if (Has31Days(i))
                {
                    LoopDays(31, i, meetings);
                }
                else if (Has30Days(i))
                {
                    LoopDays(30, i, meetings);
                }
                else
                {
                    // luty
                    LoopDays(28, i, meetings);
                }
            }

            return meetings;
        }

        private static void LoopDays(int numberOfDays, int month, ScheduleAppointmentCollection meetings)
        {
            
            for(int i=1; i<=numberOfDays; i++)
            {
                LoopHours(month, i, meetings);
            }
        }

        private static void LoopHours(int month, int day, ScheduleAppointmentCollection meetings)
        {
            int startHour = 8;
            int traningLength = 2;
            int endHour = 16;

            for(int i=startHour; i<=endHour; i += traningLength)
            {
                AddApointment(month, day, meetings, i);
            }
        }

        private static void AddApointment(int month, int day, ScheduleAppointmentCollection meetings, int startHour)
        {
            var meeting = new ScheduleAppointment();

            var startTime = Calendar.Instance;
            var endTime = Calendar.Instance;
            startTime.Set(2021, month, day, startHour, 0);
            endTime.Set(2021, month, day, startHour + 1, 45);
            meeting.StartTime = startTime;
            meeting.EndTime = endTime;
            meeting.Subject =  "Free";
            meeting.Notes = $"{2021}/{month+1}/{day}/{startHour}/{0}/{startHour+1}/45";
            meeting.Color = Color.ParseColor("#117EB4");
            meetings.Add(meeting);
        }

        private static ScheduleAppointment CreateAppointmentFromDateTime(int year, int month, int day, int startHour, int startMinute, int endHour, int endMinute)
        {
            var meeting = new ScheduleAppointment();

            var startTime = Calendar.Instance;
            var endTime = Calendar.Instance;

            startTime.Set(year, month, day, startHour, startMinute);
            endTime.Set(year, month, day, endHour, endMinute);
            meeting.StartTime = startTime;
            meeting.EndTime = endTime;
            meeting.Subject = "Busy";
            meeting.Notes = $"{year}/{month}/{day}/{startHour}/{startMinute}/{endHour}/{endMinute}";
            meeting.Color = Color.ParseColor("#f54266");
            return meeting;
        }

        private static bool Has30Days(int i)
        {
            return i == 4 || i == 6 || i == 9 || i == 11;
        }

        private static bool Has31Days(int i)
        {
            return i == 1 || i == 3 ||
                    i == 5 || i == 7 ||
                    i == 8 || i == 10 ||
                    i == 12;
        }
    }
}