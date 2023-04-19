using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Recevied")]
        PaymentRecevied,
        
        [EnumMember(Value = "Paymant Failed")]
        PaymantFailed,
        
        [EnumMember(Value = "Shipped")]
        Shipped,
        
        [EnumMember(Value = "Complete")]
        Complete
    }
}
