using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;

namespace E_Cart.Services.IServices
{
    public interface ICartService
    {
        Task<bool> CartUpsert(CartDto cartDto);

        Task<CartDto> GetUserCart(Guid userId);

        Task<bool> ApplyCoupons(CartDto cartDto);

        Task<bool> RemoveFromCart(Guid CartDetailId);
    }
}