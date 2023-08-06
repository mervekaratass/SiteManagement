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

        //bu içi bboş gelicek olan lar için oluşturduğum response mesela delete,post,put
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
    // buda içi dolu dönücek olanlar için oluşturduğum response getall,getbyid
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
    // Diğer özellikler ve constructorlar

    // Bu constructor, hem başarılı hem de başarısız durumlar için kullanılacak.
   
    public ApiResponse(bool isSuccess, string message)
    {
        Success = isSuccess;
        Message = message;
    }
   
}

public partial class ApiResponse
{
    public List<string> Errors { get; set; }

    public ApiResponse(List<ValidationFailure> validationErrors)
    {
        Success = false;
        Errors = validationErrors.Select(error => error.ErrorMessage).ToList();
    }
}


