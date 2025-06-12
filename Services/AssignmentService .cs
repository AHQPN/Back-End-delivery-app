using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Repositories;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repo;
        public AssignmentService(IAssignmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<AssignOrderResultDto> AssignOrderToNearestShipperAsync(string orderId)
        {
            return await _repo.AssignOrderToNearestShipperAsync(orderId);
        }
    }

    public interface IAssignmentService
    {
        Task<AssignOrderResultDto> AssignOrderToNearestShipperAsync(string orderId);
    }
}