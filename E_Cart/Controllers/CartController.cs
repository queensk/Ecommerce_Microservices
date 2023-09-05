using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;
using E_Cart.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Cart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ResponseDto _responseDto;
        public CartController(ICartService cartService, ResponseDto responseDto)
        {
            _cartService = cartService;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> ResponseDto(CartDto cartDto)
        {
            try
            {
                var response = await _cartService.CartUpsert(cartDto);
                if (response)
                {
                    _responseDto.Result = response;
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            
            }
            return Ok(_responseDto);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart([FromBody]Guid cartDetailsId)
        {
            try
            {
                var response = await _cartService.RemoveFromCart(cartDetailsId);
                _responseDto.Result = response;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon([FromBody]CartDto cartDto)
        {
            try
            {
                var response = await _cartService.ApplyCoupons(cartDto);
                _responseDto.Result = response;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUserCart(Guid userId)
        {
            try
            {
                var response = await _cartService.GetUserCart(userId);
                _responseDto.Result = response;
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}