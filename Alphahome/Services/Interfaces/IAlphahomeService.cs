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
        Home GetHomePage();
        //ApiResponse GetCategoryList();
        Task<string> UploadImage(IFormFile uFile);
        Task<List<string>> UploadMultiImage(IFormFileCollection formFiles);
        Task<List<Ads>> UploadVideoAds(IFormFileCollection formFiles);
        Task<Response> FindUserAsync(string userId);
        List<Ads> GetAdsVideo(int numVideo);
        List<Ads> ManageAdsVideo();
        Response DeleteAdsVideo(int id);
        Ads FindAdsById(int id);
        Response UpdateQueue(Ads ads);
        Response UploadLinkAdsVideo(AdsPost adsPost);
        List<ServiceType> GetServiceType();
        ServiceType GetServiceByOrder(int order);
        Management GetManagerPage(int offSet, int pageSize);
        List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize);
        ServiceDetail GetDetailById([FromQuery] long sid);
        List<Project> GetProjectPage(int offSet, int pageSize);
        ProjectDetail GetDetailProject(long pId);
        List<Post> GetPosts(int offSet, int pageSize);
        PostDetail GetDetailPost([FromQuery] long postId);
        Response SetNewPost([FromBody] PostParam post);
        Response SetNewService([FromBody] ServicePost service);
        Response SetNewProject(ProjectPost project);
        Response UpdatePost(PostUpdate post);
        Response UpdateService(ServiceUpdate service);
        Response UpdateProject(ProjectUpdate project);
        Response DeleteService(ServiceDelete serviceDelete);
        Response DeleteProject(ProjectDelete projectDelete);
        Response DeletePost(PostDelete postDelete);
        List<ManageService> GetServices(int offSet, int pageSize);
        AuthenticateResponse Authenticate(AlphahomeUser user, string ipAddress);
        AuthenticateResponse RefreshToken(string refresh_token, string ipAddress);
        ServiceDetail GetServiceById(long id);
        List<Search> Search(string query);
    }
}
