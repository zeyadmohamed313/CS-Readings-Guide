using Microsoft.AspNetCore.Mvc;
using System.Net;
using Core.ResponseSchema;
namespace CS_Readings_Guide.Base
{
    [ApiController]
    public class AppControllerBase:ControllerBase
    {
        #region Actions
        public ObjectResult NewResult<T>(ApiResponse<T> response)
        {
            // there is no list here
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
        #endregion
    }
}
