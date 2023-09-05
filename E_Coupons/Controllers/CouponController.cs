using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using E_Coupons.Services.IService;
using E_Coupons.Model.Dtos;
using E_Coupons.Model;
using Microsoft.AspNetCore.Authorization;

namespace E_Coupons.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponInterface _couponInterface;
        private readonly ResponseDto _responseDto;
        public CouponController(IMapper mapper, ICouponInterface couponInterface)
        {
            _couponInterface = couponInterface;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons()
        {
            var coupon = await _couponInterface.GetCouponsAsync();
            if (coupon != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Coupons Fetched Successfully";
                _responseDto.Result = coupon;
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Coupons Not Found";
            return BadRequest(_responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> CreateCoupon(CouponRequestDto couponRequest)
        {
            var newCoupon = _mapper.Map<Coupon>(couponRequest);
            var coupon = await _couponInterface.AddCouponAsync(newCoupon);
            if(string.IsNullOrWhiteSpace(coupon))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Coupon Not Created";
                return BadRequest(_responseDto);
            }
            _responseDto.IsSuccess = true;
            _responseDto.Message = "Coupon Created Successfully";
            _responseDto.Result = coupon;
            return Ok(_responseDto);
        }

        [HttpGet("GetByName{code}")]
        public async Task<ActionResult<ResponseDto>> GetCouponByName(string code)
        {
            var coupon = await _couponInterface.GetCouponByNameAsync(code); 
            if (coupon != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "";
                _responseDto.Result = coupon;
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Coupon Not Found";
            return BadRequest(_responseDto);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateCoupon(Guid id , CouponRequestDto couponRequestDto)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if(coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Coupon Not Found";
                return BadRequest(_responseDto);
            }
            var updatedCoupon = _mapper.Map(couponRequestDto, coupon);
            var updatedCouponResponse = await _couponInterface.UpdateCouponAsync(updatedCoupon);
            _responseDto.IsSuccess = true;
            _responseDto.Message = "Coupon Updated Successfully";
            _responseDto.Result = updatedCouponResponse;
            return Ok(_responseDto);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid id)
        {
            var coupon = await _couponInterface.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Coupon Not Found";
                return BadRequest(_responseDto);
            }
            var response = await _couponInterface.DeleteCouponAsync(coupon);
            _responseDto.IsSuccess = true;
            _responseDto.Message = "Coupon Deleted Successfully";
            _responseDto.Result = response;
            return Ok(_responseDto);
        }
    }        
}