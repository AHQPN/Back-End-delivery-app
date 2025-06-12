using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mobile_App.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly Tracking_ShipmentContext _context;

        public AssignmentRepository(Tracking_ShipmentContext context)
        {
            _context = context;
        }

        public async Task<AssignOrderResultDto> AssignOrderToNearestShipperAsync(string orderId)
{
    var order = await _context.Orders
        .Include(o => o.SourceLocationNavigation)
        .FirstOrDefaultAsync(o => o.OrderId.Trim() == orderId.Trim());

    if (order == null)
        return new AssignOrderResultDto
        {
            Success = false,
            Message = "Order not found",
            OrderId = orderId,
            DeliveryPersonId = null,
            Distance = 0
        };

    var orderVehicleType = order.VehicleId;

    var assignments = await _context.Assignments
        .Where(a => a.VehicleId == orderVehicleType)
        .ToListAsync();

    if (!assignments.Any())
        return new AssignOrderResultDto
        {
            Success = false,
            Message = "No assignment with matching vehicle type",
            OrderId = orderId,
            DeliveryPersonId = null,
            Distance = 0
        };

    var shipperIds = assignments.Select(a => a.DeliveryPersonId).ToList();

    var shippers = await _context.Users
        .Where(u => shipperIds.Contains(u.UserId))
        .Include(u => u.UserLocationNavigation)
        .ToListAsync();

    if (!shippers.Any())
        return new AssignOrderResultDto
        {
            Success = false,
            Message = "No shipper found",
            OrderId = orderId,
            DeliveryPersonId = null,
            Distance = 0
        };

    var sourceLocation = order.SourceLocationNavigation;
    if (sourceLocation == null)
        return new AssignOrderResultDto
        {
            Success = false,
            Message = "Order source location not found",
            OrderId = orderId,
            DeliveryPersonId = null,
            Distance = 0
        };

    double minDistance = double.MaxValue;
    User? nearestShipper = null;

    foreach (var shipper in shippers)
    {
        var loc = shipper.UserLocationNavigation;
        if (loc == null) continue;
        double distance = GetDistance(
            (double)sourceLocation.Latitude, (double)sourceLocation.Longitude,
            (double)loc.Latitude, (double)loc.Longitude
        );
        if (distance < minDistance)
        {
            minDistance = distance;
            nearestShipper = shipper;
        }
    }

    if (nearestShipper == null)
        return new AssignOrderResultDto
        {
            Success = false,
            Message = "No shipper with location found",
            OrderId = orderId,
            DeliveryPersonId = null,
            Distance = 0
        };

    // Gán shipper cho order
    order.DeliveryPersonId = nearestShipper.UserId;
    await _context.SaveChangesAsync();

    return new AssignOrderResultDto
    {
        Success = true,
        OrderId = orderId,
        DeliveryPersonId = nearestShipper.UserId,
        Distance = minDistance,
        Message = "Assigned successfully"
    };
}

        private double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            throw new NotImplementedException();
        }
    }
}