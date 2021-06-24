using System.Threading.Tasks;

namespace ZnanyTrener_Android.Presenters
{
    public interface IAddCertificatePresenter
    {
        string Institution { get; set; }
        string Number { get; set; }

        Task AddCertificateAsync();
    }
}