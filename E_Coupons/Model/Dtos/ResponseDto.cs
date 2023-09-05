using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Coupons.Model.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}