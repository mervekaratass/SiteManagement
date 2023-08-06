using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiteManagement.Base;

    public partial class ApiResponse
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        //this is the response I created for the ones that will be empty, for example delete,post,put
        public ApiResponse(string message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Success = true;
            }
            else
            {
                Success = false;
                Message = message;
            }
        }

        public bool Success { get; set; }
        public string Message { get; set; }

      
    }
      // this is the response I created for those who will return full, getall, getbyid
    public partial class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }

        public ApiResponse(bool isSuccess)
        {
            Success = isSuccess;
            Response = default;
            Message = isSuccess ? "Success" : "Error";
        }
        public ApiResponse(T data)
        {
            Success = true;
            Response = data;
            Message = "Success";
        }
        public ApiResponse(string message)
        {
            Success = false;
            Response = default;
            Message = message;
        }
    }
public partial class ApiResponse
{


    // This constructor will be used for both pass and fail.

    public ApiResponse(bool isSuccess, string message)
    {
        Success = isSuccess;
        Message = message;
    }
   
}

public partial class ApiResponse
{    //This is for errors in fluent validation
    public List<string> Errors { get; set; }

    public ApiResponse(List<ValidationFailure> validationErrors)
    {
        Success = false;
        Errors = validationErrors.Select(error => error.ErrorMessage).ToList();
    }
}


