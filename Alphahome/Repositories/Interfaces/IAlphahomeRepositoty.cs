﻿using System;
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
        Home GetHomePage();
        //ApiResponse GetCategoryList();
        Task<string> UploadImage(IFormFile formFile);
        Task<List<string>> UploadMultiImage(IFormFileCollection formFiles);
        Task<List<Ads>> UploadVideoAds(IFormFileCollection formFiles);
        Task<Response> FindUserAsync(string userId);
        Response DeleteAdsVideo(int id);
        List<Ads> GetAdsVideo(int numVideo);
        Ads FindAdsById(int id);
        Response UpdateQueue(Ads ads);
        List<Ads> ManageAdsVideo();
        Response UploadLinkAdsVideo(AdsPost adsPost);
        List<ServiceType> GetServiceType();
        ServiceType GetServiceByOrder(int order);
        Management GetManagerPage(int offset, int pagesize);
        List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize);
        List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize);
        ServiceDetail GetDetailById(long sid);
        List<Project> GetProjectPage(int offSet, int pageSize);
        ProjectDetail GetDetailProject(long pId);
        List<Post> GetPosts(int offSet, int pageSize);
        PostDetail GetDetailPost(long postId);
        Response SetNewPost(PostParam post);
        Response SetNewService(ServicePost service);
        Response SetNewProject(ProjectPost project);
        Response UpdatePost(PostUpdate post);
        Response UpdateService(ServiceUpdate service);
        Response UpdateProject(ProjectUpdate project);
        Response DeleteService(ServiceDelete serviceDelete);
        Response DeleteProject(ProjectDelete projectDelete);
        Response DeletePost(PostDelete postDelete);
        List<ManageService> GetServices(int offSet, int pageSize);
        UserModelGet Authenticate(AlphahomeUser user);
        Response UpdateUserToken(string userId, string refresh_token);
        UserModelGet FindUserByRefreshToken(string refresh_token);
        ServiceDetail GetServiceById(long id);
        List<Search> Search(string query);
    }
}
