using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Alphahome.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        public ManagerController(IAlphahomeService alphahomeService)
        {
            _alphahomeService = alphahomeService;
        }
        /// <summary>
        /// Lấy ra các loại dịch vụ
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceType")]
        public IActionResult getServiceType()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetServiceType();
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy ra các dự án
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetProjectPage")]
        public IActionResult GetProjectPage(int offSet, int pageSize)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetProjectPage(offSet, pageSize);
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy ra chi tiết dự án
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailProject")]
        public IActionResult GetDetailProject(long pId)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetDetailProject(pId);
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy hỗn hợp dịch vụ
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetServices")]
        public IActionResult GetServices(int offSet, int pageSize)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetServices(offSet, pageSize);
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy ra tin tức
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetPosts")]
        public IActionResult GetPosts(int offSet, int pageSize)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetPosts(offSet, pageSize);
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy ra dịch vụ + dự án + tin tức
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetManagerPage")]
        public IActionResult GetManagerPage(int offSet, int pageSize)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetManagerPage(offSet, pageSize);
            return new JsonResult(data);
        }

        /// <summary>
        /// Lấy ra chi tiết dịch vụ
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailById")]
        public IActionResult GetDetailById(long sid, string serviceTypeId)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetDetailById(sid, serviceTypeId);
            return new JsonResult(data);
        }

        /// <summary>
        /// Thêm vào dịch vụ
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost("SetService")]
        public IActionResult SetService([FromBody] ServicePost service)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.SetNewService(service);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Thêm dự án
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost("SetProject")]
        public IActionResult SetProject([FromBody] ProjectPost project)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.SetNewProject(project);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Thêm tin tức
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost("SetPost")]
        public IActionResult SetPost([FromBody] PostParam post)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.SetNewPost(post);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Xóa dịch vụ
        /// </summary>
        /// <param name="serviceDelete"></param>
        /// <returns></returns>
        [HttpDelete("DeleteService")]
        public IActionResult DeleteService([FromBody] ServiceDelete serviceDelete)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.DeleteService(serviceDelete);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Xóa dự án
        /// </summary>
        /// <param name="projectDelete"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProject")]
        public IActionResult DeleteProject([FromBody] ProjectDelete projectDelete)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.DeleteProject(projectDelete);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Xóa tin tức
        /// </summary>
        /// <param name="postDelete"></param>
        /// <returns></returns>
        [HttpDelete("DeletePost")]
        public IActionResult DeletePost([FromBody] PostDelete postDelete)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.DeletePost(postDelete);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }
    }
}
