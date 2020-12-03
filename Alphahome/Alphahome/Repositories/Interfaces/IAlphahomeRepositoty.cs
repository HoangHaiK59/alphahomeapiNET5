using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphahome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alphahome.Repositories.Interfaces
{
    public interface IAlphahomeRepositoty
    {
        //ApiResponse GetHomePage();
        //ApiResponse GetCategoryList();
        Task<string> UploadImage(IFormFile formFile);
        Task<List<string>> UploadMultiImage(IFormFileCollection formFiles);
        List<ServiceType> GetServiceType();
        ServiceType GetServiceByOrder(int order);
        Management GetManagerPage(int offset, int pagesize);
        List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize);
        ServiceDetail GetDetailById(long sid, string serviceTypeId);
        List<Project> GetProjectPage(int offSet, int pageSize);
        ProjectDetail GetDetailProject(long pId);
        List<Post> GetPosts(int offSet, int pageSize);
        PostDetail GetDetailPost(long postId);
        Response SetNewPost(PostParam post);
        Response SetNewService(ServicePost service);
        Response SetNewProject(ProjectPost project);
        Response DeleteService(ServiceDelete serviceDelete);
        Response DeleteProject(ProjectDelete projectDelete);
        Response DeletePost(PostDelete postDelete);
        List<ManageService> GetServices(int offSet, int pageSize);
        Response Authenticate(AlphahomeUser user);
    }
}
