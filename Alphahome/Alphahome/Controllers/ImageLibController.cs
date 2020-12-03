using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageLibController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        public ImageLibController(IAlphahomeService alphahomeService)
        {
            _alphahomeService = alphahomeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpGet("Image")]
        public IActionResult GetImage ([FromForm] IFormFile formFile)
        {
            var result = _alphahomeService.UploadImage(formFile).Result;
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }

            return Ok(result);
        }

    }
}
