using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;
using E_Cart.Services.IServices;
using Newtonsoft.Json;

namespace E_Cart.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpService;
        public CouponService(IHttpClientFactory httpService)
        {
            _httpService = httpService;
        }

        public async Task<CouponDto> GetCouponData(string CouponCode)
        {
            var client = _httpService.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/Coupon/GetByName/{CouponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return new CouponDto();
        }
    }
}