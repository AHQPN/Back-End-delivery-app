namespace Backend_Mobile_App.DTOs
{
    public class OrderStatisticsDto
    {
        public int TongDonHang { get; set; }
        public int TongShipper { get; set; }
        public int TongKhach { get; set; }
        public int TongPhuongTien { get; set; }
        public Dictionary<string, decimal> DoanhThuTheoThang { get; set; }
    }
}