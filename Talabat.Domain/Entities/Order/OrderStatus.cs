using System.Runtime.Serialization;

namespace Talabat.Domain.Entities.Order;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Payment Received")]
    PaymentReceived,
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed,
    [EnumMember(Value = "Shipped")]
    Shipped,
    [EnumMember(Value = "Delivered")]
    Delivered,
    [EnumMember(Value = "Cancelled")]
    Cancelled
}
