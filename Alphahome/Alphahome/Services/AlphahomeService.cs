using Alphahome.Models;
using Alphahome.Repositories.Interfaces;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Services
{
    public class AlphahomeService: IAlphahomeService
    {
        private readonly IAlphahomeRepositoty _alphahomeRepo;
        private IConfiguration _configuration;
        public AlphahomeService(IAlphahomeRepositoty alphahomeRepo)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            _alphahomeRepo = alphahomeRepo;
        }
        //public ApiResponse GetHomePage()
        //{
        //    return _alphahomeRepo.GetHomePage();
        //}
        //public ApiResponse GetCategoryList()
        //{
        //    return _alphahomeRepo.GetCategoryList();
        //}
        public List<ServiceType> GetServiceType()
        {
            return _alphahomeRepo.GetServiceType();
        }
        public ServiceType GetServiceByOrder(int order)
        {
            return _alphahomeRepo.GetServiceByOrder(order);
        }
        public Management GetManagerPage(int offSet, int pageSize)
        {
            return _alphahomeRepo.GetManagerPage(offSet, pageSize);
        }
        public List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            return _alphahomeRepo.GetFurniturePage(serviceTypeId, offset, pagesize);
        }
        public List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize)
        {
            return _alphahomeRepo.GetWoodenPage(serviceTypeId, offset, pagesize);
        }
        public List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            return _alphahomeRepo.GetContructionFurniturePage(serviceTypeId, offset, pagesize);
        }
        public List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize)
        {
            return _alphahomeRepo.GetContructionHousePage(serviceTypeId, offset, pagesize);
        }
        public ServiceDetail GetDetailById([FromQuery] long sid, [FromQuery] string serviceTypeId)
        {
            return _alphahomeRepo.GetDetailById(sid, serviceTypeId);
        }
        public List<Project> GetProjectPage(int offSet, int pageSize)
        {
            return _alphahomeRepo.GetProjectPage(offSet, pageSize);
        }
        public ProjectDetail GetDetailProject(long pId)
        {
            return _alphahomeRepo.GetDetailProject(pId);
        }
        public List<Post> GetPosts(int offSet, int pageSize)
        {
            return _alphahomeRepo.GetPosts(offSet, pageSize);
        }
        public PostDetail GetDetailPost(long postId)
        {
            return _alphahomeRepo.GetDetailPost(postId);
        }
        public Response SetNewPost(PostParam post)
        {
            return _alphahomeRepo.SetNewPost(post);
        }
        public Response SetNewService(ServicePost service)
        {
            return _alphahomeRepo.SetNewService(service);
        }
        public Response SetNewProject(ProjectPost project)
        {
            return _alphahomeRepo.SetNewProject(project);
        }

        public List<ManageService> GetServices(int offSet, int pageSize)
        {
            return _alphahomeRepo.GetServices(offSet, pageSize);
        }

        public Task<string> UploadImage(IFormFile uFile)
        {
            return _alphahomeRepo.UploadImage(uFile);
        }
        public Task<List<string>> UploadMultiImage(IFormFileCollection formFiles)
        {
            return _alphahomeRepo.UploadMultiImage(formFiles);
        }
        public Response DeleteService(ServiceDelete serviceDelete)
        {
            return _alphahomeRepo.DeleteService(serviceDelete);
        }
        public Response DeleteProject(ProjectDelete projectDelete)
        {
            return _alphahomeRepo.DeleteProject(projectDelete);
        }
        public Response DeletePost(PostDelete postDelete)
        {
            return _alphahomeRepo.DeletePost(postDelete);
        }
        public UserModelGet Authenticate(AlphahomeUser user)
        {
            return _alphahomeRepo.Authenticate(user);
        }

        //public bool RevokeToken(string token, string ipAddress)
        //{
        //    var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    // return false if no user found with token
        //    if (user == null) return false;

        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    // return false if token is not active
        //    if (!refreshToken.IsActive) return false;

        //    // revoke token and save
        //    refreshToken.Revoked = DateTime.UtcNow;
        //    refreshToken.RevokedByIp = ipAddress;
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    return true;
        //}
    }
}
