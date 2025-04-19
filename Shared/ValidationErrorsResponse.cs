using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared
{
    public class ValidationErrorsResponse
    {
        public int StatusCode { get; set; }=StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "ValidationError";
        public IEnumerable<ValidtionError> Errors { get; set; }
    }
}
