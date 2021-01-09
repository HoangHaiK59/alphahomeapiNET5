using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Alphahome.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        private readonly IWebHostEnvironment _env;
        public ManagerController(IAlphahomeService alphahomeService, IWebHostEnvironment environment)
        {
            _alphahomeService = alphahomeService;
            _env = environment;
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
        /// Lấy ra chi tiết tin tức
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailPost")]
        public ActionResult GetDetailPost(long postId)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetDetailPost(postId);
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetServiceById")]
        public IActionResult GetServiceById(long id)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.GetServiceById(id);
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
        /// Cập nhật bài viết
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost("UpdatePost")]
        public IActionResult UpdatePost([FromBody] PostUpdate post)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.UpdatePost(post);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Cập nhật thông tin dịch vụ
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost("UpdateService")]
        public IActionResult UpdateService([FromBody] ServiceUpdate service)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.UpdateService(service);
            if (data.valid)
            {
                return new JsonResult(data);
            }
            return BadRequest(data);
        }

        /// <summary>
        /// Cập nhật thông tin dự án
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost("UpdateProject")]
        public IActionResult UpdateProject([FromBody] ProjectUpdate project)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var data = _alphahomeService.UpdateProject(project);
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
        /// <summary>
        /// Thư viện hình ảnh
        /// </summary>
        /// <returns></returns>
        [HttpGet("Library")]
        public IActionResult Library()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var files = _env.ContentRootFileProvider.GetDirectoryContents("wwwroot/images");
            var listFileName = new List<string>();
            foreach(var file in files)
            {
                listFileName.Add("/images/" + file.Name);
            }

            return Ok(listFileName);
        }
        /// <summary>
        /// Quản lý quảng cáo
        /// </summary>
        /// <returns></returns>
        [HttpGet("ManageAdsVideo")]
        public IActionResult ManageAdsVideo()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var list = _alphahomeService.ManageAdsVideo();
            return Ok(list);
        }

        /// <summary>
        /// Lưu link video
        /// </summary>
        /// <param name="adsPost"></param>
        /// <returns></returns>
        [HttpPost("UploadLinkAdsVideo")]
        public IActionResult UploadLinkAdsVideo([FromBody] AdsPost adsPost)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var response = _alphahomeService.UploadLinkAdsVideo(adsPost);
            if (response.valid)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Xóa video ads
        /// </summary>
        /// <param name="adsDelete"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAdsVideo")]
        public IActionResult DeleteAdsVideo([FromBody] AdsDelete adsDelete)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var deleteAds = _alphahomeService.FindAdsById(adsDelete.id);
            var fileToDelete = Path.Combine(_env.WebRootPath, "ads\\", deleteAds.name);
            if ((System.IO.File.Exists(fileToDelete)))
            {
                System.IO.File.Delete(fileToDelete);
            }
            var response = _alphahomeService.DeleteAdsVideo(adsDelete.id);
            if (response.valid)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Thay đổi thứ tự hàng chờ
        /// </summary>
        /// <param name="ads"></param>
        /// <returns></returns>
        [HttpPut("SetAdsQueue")]
        public IActionResult SetAdsQueue([FromBody] Ads ads)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }
            var response = _alphahomeService.UpdateQueue(ads);
            if (response.valid)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
