using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Upload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        public UploadController(IAlphahomeService alphahomeService)
        {
            _alphahomeService = alphahomeService;
        }

        /// <summary>
        /// Thêm ảnh
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        public IActionResult UploadFile ([FromForm] IFormFile formFile)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var result = _alphahomeService.UploadImage(formFile).Result;
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Thêm nhiều ảnh
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        [HttpPost("UploadMultiImage")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadMultiImage([FromForm] IFormFileCollection formFiles)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var result = _alphahomeService.UploadMultiImage(formFiles).Result;
            if (result.Count > 0)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Tải lên video ads
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        [HttpPost("UploadVideoAds")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadVideoAds([FromForm] IFormFileCollection formFiles)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var result = _alphahomeService.UploadVideoAds(formFiles).Result;
            if (result.Count > 0)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
