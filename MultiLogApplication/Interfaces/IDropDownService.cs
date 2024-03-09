using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;

namespace MultiLogApplication.Interfaces
{
    public interface IDropDownService
    {
        Task<ReturnType<DropDownDetails>> TransactionTypes(TransactionType entity);
        Task<ReturnType<DropDownDetails>> StatusTypes(GetStatusType entity);
    }
}
