using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Products.Model;
using E_Products.Model.Dtos;
using E_Products.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductInterface _productInterface;
        private readonly ResponseDto _responseDto;
        public ProductController(IMapper mapper, IProductInterface productInterface)
        {
            _mapper = mapper;
            _productInterface = productInterface;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllProducts()
        {
            var products = await _productInterface.GetProductsAsync();
            if (products != null)
            {
                _responseDto.Result = products;
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Products Fetched Successfully";
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Products Not Fetched";
            return Ok(_responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> CreateProduct(ProductRequestDto productDto)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(productDto);
                var product = await _productInterface.AddProductAsync(newProduct);
                if (!string.IsNullOrWhiteSpace(product))
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Product Created Successfully";
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Product Not Created";
                return Ok(_responseDto);
            }catch(Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }

        }

        [HttpGet("GetById({id})")]
        public async Task<ActionResult<ResponseDto>> GetProductById(Guid id)
        {
            try
            {
                var product = await _productInterface.GetProductByIdAsync(id);
                if (product != null)
                {
                    _responseDto.Result = _mapper.Map<ProductRequestDto>(product);
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Product Fetched Successfully";
                    return Ok(_responseDto);
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Product Not Fetched";
                return BadRequest(_responseDto);
            
            }
            catch (System.Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid Id, ProductRequestDto productDto)
        {
            try
            {
                var exitingProduct = await _productInterface.GetProductByIdAsync(Id);

                if (productDto != null)
                {
                    var updatedProduct = _mapper.Map(productDto, exitingProduct);
                    var response = await _productInterface.UpdateProductAsync(updatedProduct);
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "Product Updated Successfully";
                        _responseDto.Result = response;
                        return Ok(_responseDto);
                    }
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Product Not Updated";
                return Ok(_responseDto);
            }
            catch (System.Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _productInterface.GetProductByIdAsync(id);
                if (product != null)
                {
                    var response = await _productInterface.DeleteProductAsync(product);
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "Product Deleted Successfully";
                        _responseDto.Result = response;
                        return Ok(_responseDto);
                    }
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Product Not Deleted";
                    return BadRequest(_responseDto);
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Product Not Found";
                return BadRequest(_responseDto);
            }
            catch (System.Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = e.Message;
                return BadRequest(_responseDto);
            }
        }
    }
}