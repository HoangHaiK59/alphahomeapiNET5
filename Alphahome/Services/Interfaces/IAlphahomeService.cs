using Alphahome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Services.Interfaces
{
    public interface IAlphahomeService
    {
        //ApiResponse GetHomePage();
        //ApiResponse GetCategoryList();
        Task<string> UploadImage(IFormFile uFile);
        Task<List<string>> UploadMultiImage(IFormFileCollection formFiles);
        List<ServiceType> GetServiceType();
        ServiceType GetServiceByOrder(int order);
        Management GetManagerPage(int offSet, int pageSize);
        List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize);
        ServiceDetail GetDetailById([FromQuery] long sid, [FromQuery] string serviceTypeId);
        List<Project> GetProjectPage(int offSet, int pageSize);
        ProjectDetail GetDetailProject(long pId);
        List<Post> GetPosts(int offSet, int pageSize);
        PostDetail GetDetailPost([FromQuery] long postId);
        Response SetNewPost([FromBody] PostParam post);
        Response SetNewService([FromBody] ServicePost service);
        Response SetNewProject(ProjectPost project);
        Response DeleteService(ServiceDelete serviceDelete);
        Response DeleteProject(ProjectDelete projectDelete);
        Response DeletePost(PostDelete postDelete);
        List<ManageService> GetServices(int offSet, int pageSize);
        AuthenticateResponse Authenticate(AlphahomeUser user, string ipAddress);

    }
}
