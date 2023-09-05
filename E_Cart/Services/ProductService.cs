using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Cart.Model.Dto;
using E_Cart.Services.IServices;
using Newtonsoft.Json;

namespace E_Cart.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var client = _clientFactory.CreateClient("Product");
            var response = client.GetAsync("api/Product");
            var content = await response.Result.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}