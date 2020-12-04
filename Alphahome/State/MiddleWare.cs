using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alphahome.State
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseMiddleware> _logger;
        public ResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ResponseMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            var originBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                // Process inner middlewares and return result.
                await _next(context);

                var ModelState = context.Features.Get<ModelStateFeature>()?.ModelState;
                if (ModelState == null)
                {
                    return;
                }

                responseBody.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(responseBody))
                {
                    // Get action result come from mvc pipeline
                    var strActionData = streamReader.ReadToEnd();
                    var objActionData = JsonConvert.DeserializeObject(strActionData);
                    context.Response.Body = originBody;
                    var listStatusCode= new[] { 200, 204 };

                    var status = listStatusCode.Contains(context.Response.StatusCode) ? "success" : "error";

                    // Create uniuqe shape for all responses.
                    var responseModel = new GenericResponse(objActionData, (HttpStatusCode)context.Response.StatusCode, context.Items?["Message"]?.ToString(), status);

                    // if (!ModelState.IsValid) => Get error message

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage)
                            .ToList();
                        responseModel.Data = null;
                        responseModel.Message = String.Join(" ; ", errors);
                    }

                    // Set all response code to 200 and keep actual status code inside wrapped object.
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseModel));
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseMiddlewareWrapper
    {
        public static IApplicationBuilder UseResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMiddleware>();
        }
    }

    [Serializable]
    [DataContract]
    public class GenericResponse
    {
        public GenericResponse(object data, HttpStatusCode statusCode, string message, string status)
        {
            StatusCode = (int)statusCode;
            Data = data;
            Message = message;
            Status = status;
        }
        [DataMember(Name = "data")]
        public object Data { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; } = "V1.0";
    }
}
