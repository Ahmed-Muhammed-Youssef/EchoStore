using Core.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Order> CreatePaymentIntent(int orderId);
        Task<Order> UpdateOrderPaymentSucceeded(string intentId);
        Task<Order> UpdateOrderPaymentFailed(string intentId);
    }
}
