using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Cart.Data;
using E_Cart.Model;
using E_Cart.Model.Dto;
using E_Cart.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace E_Cart.Services
{
    public class CartService : ICartService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _Mapper;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public CartService(AppDbContext context, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _context = context;
            _Mapper = mapper;
            _productService = productService;
            _couponService = couponService;
        }
        public async Task<bool> ApplyCoupons(CartDto cartDto)
        {
            CartHeader CartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId ==cartDto.CartHeader.UserId);
            CartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            _context.CartHeaders.Update(CartHeaderFromDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CartUpsert(CartDto cartDto)
        {
            // create user header for first time users
            try{
                CartHeader cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    var newCartHeader = _Mapper.Map<CartHeader>(cartDto.CartHeader);
                    _context.CartHeaders.Add(newCartHeader);
                    await _context.SaveChangesAsync();

                    // create user detail for first time users
                    cartDto.CartDetails.First().CartHeaderId = newCartHeader.CartHeaderId;
                    var newCartDetail = _Mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    _context.CartDetails.Add(newCartDetail);
                    await _context.SaveChangesAsync();
                    
                    // update multiple products
                    return true;
                }
                else 
                {
                    // add a new product to the cart or updating product count
                    CartDetails cartDetailFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId && u.ProductId == cartDto.CartDetails.First().ProductId);
                    if (cartDetailFromDb == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        var newCartDetail = _Mapper.Map<CartDetails>(cartDto.CartDetails.First());
                        await _context.CartDetails.AddAsync(newCartDetail);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        cartDetailFromDb.Count += cartDto.CartDetails.First().Count;
                        _context.CartDetails.Update(cartDetailFromDb);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<CartDto> GetUserCart(Guid userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            var cartDetails = await _context.CartDetails.AsNoTracking().Where(u => u.CartHeaderId == cartHeader.CartHeaderId).ToListAsync();
            CartDto cartDto = new CartDto()
            {
                CartHeader = _Mapper.Map<CartHeaderDto>(cartHeader),
                CartDetails = _Mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails)
            };

            var products = await _productService.GetProductsAsync();
            foreach (var cartDetail in cartDto.CartDetails)
            {
                cartDetail.Product = products.FirstOrDefault(u => u.ProductId == cartDetail.ProductId);
                cartDto.CartHeader.CartTotal += (int) cartDetail.Product.Price * cartDetail.Count;
            }

            if (!string.IsNullOrWhiteSpace(cartDto.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetCouponData(cartDto.CartHeader.CouponCode);
                if (coupon != null && cartDto.CartHeader.CartTotal > coupon.CouponMinAmount)
                {
                    cartDto.CartHeader.Discount = (int) (cartDto.CartHeader.CartTotal * coupon.CouponAmount / 100);
                    cartDto.CartHeader.CartTotal -= cartDto.CartHeader.Discount;
                }
            }
            return cartDto;
        }

        public async Task<bool> RemoveFromCart(Guid CartDetailId)
        {
            CartDetails cartDetail = _context.CartDetails.FirstOrDefault(u => u.CartDetailsId == CartDetailId);
            var itemsCount = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();
            _context.CartDetails.Remove(cartDetail);

            if (itemsCount == 1)
            {
                _context.CartHeaders.Remove(_context.CartHeaders.FirstOrDefault(c => c.CartHeaderId == cartDetail.CartHeaderId));
            }
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}