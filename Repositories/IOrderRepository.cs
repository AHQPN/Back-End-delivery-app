using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.DTOs;

namespace Backend_Mobile_App.Repositories
{
    public interface IOrderRepository
    {
        public Task<string> AddOrderAsync(OrderCreateDto orderCreateDto);
        public Task<List<OrderCreateDto>> GetAllOders();
        public Task<OrderResponseDTO> GetOrderByOrderIdAsync(string orderId);
        public Task<List<OrderResponseDTO>> GetAllOdersByCustomerId(string customerId);
        public Task<List<CategoryDTO>> GetAllCategoriesAsync();
        public Task<List<ServiceDTO>> GetAllServicesAsync();
        public Task<List<SizeDTO>> GetAllSizesAsync();
        public Task<List<VehicleDTO>> GetAllVehiclesAsync();
    }
}
