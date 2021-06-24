using Com.Syncfusion.Schedule;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Requests;
using ZnanyTrener_Android.Models.Responses;

namespace ZnanyTrener_Android.Presenters
{
    public interface IMySchedulePresenter
    {
        List<TrainingResponse> Trainings { get; }
        bool CanAddTraining { get; }

        Task<ScheduleAppointmentCollection> GetAppointmentsAsync();
        TrainingToAddRequest GetTrainingToAddRequest(ScheduleAppointment appointment);
    }
}