using System;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ICartRepository CartRepository { get;  }
        public IDeliveryMethodRepository DeliveryMethodRepository { get;  }
        public IOrderRepository OrderRepository { get;  }
        public IProductRepository ProductRepository { get;  }
        public bool HasChanges();
        public Task<int> Complete();
    }
}
