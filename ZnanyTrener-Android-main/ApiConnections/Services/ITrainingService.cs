using System.Net.Http;
using System.Threading.Tasks;
using ZnanyTrener_Android.Models.Requests;

namespace ZnanyTrener_Android.ApiConnections.Services
{
    public interface ITrainingService
    {
        Task<HttpResponseMessage> AddTrainingAsync(TrainingToAddRequest request);
        Task<string> GetCoachAppointments(int coachId);
        Task<string> GetUserAppointments(int userId);
    }
}