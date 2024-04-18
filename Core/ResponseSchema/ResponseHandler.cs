using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ResponseSchema
{
    public class ResponseHandler
    {
        public ResponseHandler()
        {
        }
        public ApiResponse<T> Deleted<T>(string massege = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = massege == null ? "Deleted": massege
            };
        }
        public ApiResponse<T> Success<T>(T entity, object Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Operation Is Done Successfully",
                Meta = Meta
            };
        }
        public ApiResponse<T> Unauthorized<T>(string massege = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = massege == null ? "Un Authorized" : massege
            };
        }
        public ApiResponse<T> BadRequest<T>(string Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }
        public ApiResponse<T> UnprocessableEntity<T>(string Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "UnprocessableEntity" : Message
            };
        }

        public ApiResponse<T> NotFound<T>(string message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public ApiResponse<T> Created<T>(T entity, object Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created Successfully",
                Meta = Meta
            };
        }
    }
}
