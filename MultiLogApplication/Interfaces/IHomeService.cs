using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Dashboard;

namespace MultiLogApplication.Interfaces
{
    public interface IHomeService
    {
        Task<ReturnType<DashboardImages>> DashboardImages();
        Task<ReturnType<string>> InsertDahboardImages(InsertDashboardImages obj);
        Task<ReturnType<string>> DeleteDahboardImages(string DocId);

    }
}
