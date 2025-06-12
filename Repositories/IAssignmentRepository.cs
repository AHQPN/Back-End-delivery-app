using Backend_Mobile_App.DTOs;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Repositories
{
    public interface IAssignmentRepository
    {
        Task<AssignOrderResultDto> AssignOrderToNearestShipperAsync(string orderId);
    }
}