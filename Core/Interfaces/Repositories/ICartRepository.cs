﻿using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<bool> CreateUpdateCart(Cart cart);
        Task<Cart> GetCart(string id);
        Task<bool> DeleteCart(Cart cart);
    }
}
