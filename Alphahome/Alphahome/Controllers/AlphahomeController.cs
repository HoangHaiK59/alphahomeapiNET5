using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alphahome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlphahomeController : ControllerBase
    {
        private readonly IAlphahomeService _alphahomeService;
        public AlphahomeController(IAlphahomeService alphahomeService)
        {
            _alphahomeService = alphahomeService;
        }
       /// <summary>
       ///  Lấy ra loại dịch vụ
       /// </summary>
       /// <returns></returns>
        [HttpGet("GetServiceType")]
        public ActionResult getServiceType()
        {
            var data = _alphahomeService.GetServiceType();
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra dịch vụ theo thứ tự
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpGet("GetServiceByOrder")]
        public ActionResult getServiceByOrder([FromQuery] int order)
        {
            var data = _alphahomeService.GetServiceByOrder(order);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra dự án
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetProjectPage")]
        public ActionResult GetProjectPage(int offSet, int pageSize)
        {
            var data = _alphahomeService.GetProjectPage(offSet, pageSize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra chi tiết dự án
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailProject")]
        public ActionResult GetDetailProject(long pId)
        {
            var data = _alphahomeService.GetDetailProject(pId);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra thiết kế nội thất, ngoại thất
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <param name="offset"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet("GetFurniturePage")]
        public ActionResult GetFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            var data = _alphahomeService.GetFurniturePage(serviceTypeId, offset, pagesize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra sản phẩm đồ gỗ
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <param name="offset"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet("GetWoodenPage")]
        public ActionResult GetWoodenPage(string serviceTypeId, int offset, int pagesize)
        {
            var data = _alphahomeService.GetWoodenPage(serviceTypeId, offset, pagesize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra thi công nội thất, ngoại thất
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <param name="offset"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet("GetContructionFurniturePage")]
        public ActionResult GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            var data = _alphahomeService.GetContructionFurniturePage(serviceTypeId, offset, pagesize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra thiết kế nhà trọn gói
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <param name="offset"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet("GetContructionHousePage")]
        public ActionResult GetContructionHousePage(string serviceTypeId, int offset, int pagesize)
        {
            var data = _alphahomeService.GetContructionHousePage(serviceTypeId, offset, pagesize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra dịch vụ theo id
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailById")]
        public ActionResult GetDetailById(long sid, string serviceTypeId)
        {
            var data = _alphahomeService.GetDetailById(sid, serviceTypeId);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra tin tức
        /// </summary>
        /// <param name="offSet"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetPosts")]
        public ActionResult GetPosts(int offSet, int pageSize)
        {
            var data = _alphahomeService.GetPosts(offSet, pageSize);
            return Ok(data);
        }

        /// <summary>
        /// Lấy ra chi tiết tin tức
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailPost")]
        public ActionResult GetDetailPost(long postId)
        {
            var data = _alphahomeService.GetDetailPost(postId);
            return Ok(data);
        }
    }
}
