using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.DTOs.ResponseDTOs
{
    public class ResultDto:IResult
    {
        public ResultDto(int statusCode, bool success, string message)
        {
            this.statusCode = statusCode;
            this.success = success;
            this.message = message;
        }
        public int? statusCode { get; init; } = 200;
        public bool success { get; init; } = true;
        public string message { get; init; } = "Successfully";

        public Task ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
