using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RateEngine.Core.Constants;

namespace RateEngine.Api.Functions.Functions
{
    public abstract class BaseFunction<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IMediator _mediator;

        protected BaseFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Execute(TRequest request)
        {
            try
            {
                var users = await _mediator.Send(request);

                return new OkObjectResult(users);
            }
            catch (ValidationException validationException)
            {
                var result = new { errors = validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }) };

                return new UnprocessableEntityObjectResult(result);
            }
           
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = MimeTypes.Json,
                    Content = JsonConvert.SerializeObject(new
                    {
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException?.Message,
                        StackTrace = ex.StackTrace
                    })
                };
            }
        }
    }
}
