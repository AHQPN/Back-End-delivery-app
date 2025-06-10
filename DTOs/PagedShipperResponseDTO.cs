namespace Backend_Mobile_App.DTOs
{
    public class PagedShipperResponseDTO
    {
        public List<ShipperDTO> Data { get; set; }
        public int TotalPages { get; set; }
    }
}
