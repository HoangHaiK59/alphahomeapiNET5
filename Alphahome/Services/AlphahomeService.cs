using Alphahome.Models;
using Alphahome.Repositories.Interfaces;
using Alphahome.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alphahome.Services
{
    public class AlphahomeService: IAlphahomeService
    {
        private readonly IAlphahomeRepositoty _alphahomeRepo;
        private IConfiguration _configuration;
        public AlphahomeService(IAlphahomeRepositoty alphahomeRepo, IConfiguration config)
        {
            //IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //_configuration = builder.Build();
            _configuration = config;
            _alphahomeRepo = alphahomeRepo;
        }
        public Home GetHomePage()
        {
            return _alphahomeRepo.GetHomePage();
        }
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
        public ServiceDetail GetDetailById([FromQuery] long sid)
        {
            return _alphahomeRepo.GetDetailById(sid);
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
        public Response UpdatePost(PostUpdate post)
        {
            return _alphahomeRepo.UpdatePost(post);
        }
        public Response UpdateService(ServiceUpdate service)
        {
            return _alphahomeRepo.UpdateService(service);
        }
        public Response UpdateProject(ProjectUpdate project)
        {
            return _alphahomeRepo.UpdateProject(project);
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
        public Task<List<Ads>> UploadVideoAds(IFormFileCollection formFiles)
        {
            return _alphahomeRepo.UploadVideoAds(formFiles);
        }
        public List<Ads> GetAdsVideo(int numVideo)
        {
            return _alphahomeRepo.GetAdsVideo(numVideo);
        }
        public List<Ads> ManageAdsVideo()
        {
            return _alphahomeRepo.ManageAdsVideo();
        }
        public Response DeleteAdsVideo(int id)
        {
            return _alphahomeRepo.DeleteAdsVideo(id);
        }
        public Ads FindAdsById(int id)
        {
            return _alphahomeRepo.FindAdsById(id);
        }

        public Response UpdateQueue(Ads ads)
        {
            return _alphahomeRepo.UpdateQueue(ads);
        }
        public Response UploadLinkAdsVideo(AdsPost adsPost)
        {
            return _alphahomeRepo.UploadLinkAdsVideo(adsPost);
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
        public ServiceDetail GetServiceById(long id)
        {
            return _alphahomeRepo.GetServiceById(id);
        }
        public List<Search> Search(string query)
        {
            return _alphahomeRepo.Search(query);
        }
        public Task<Response> FindUserAsync(string userId)
        {
            return _alphahomeRepo.FindUserAsync(userId);
        }
        public AuthenticateResponse Authenticate(AlphahomeUser user, string ipAddress)
        {
            var userResponse = _alphahomeRepo.Authenticate(user);
            if (userResponse == null) return null;
            var token = generateJSONWebToken(userResponse);
            var refreshToken = generateRefreshToken(ipAddress);
            _alphahomeRepo.UpdateUserToken(userResponse.userId, refreshToken.Token);
            return new AuthenticateResponse(userResponse, token, refreshToken.Token);
        }

        public AuthenticateResponse RefreshToken(string refresh_token, string ipAddress)
        {
            var user = _alphahomeRepo.FindUserByRefreshToken(refresh_token);
            if (user == null) return null;
            //var newRefreshToken = generateRefreshToken(ipAddress);
            //_alphahomeRepo.UpdateUserToken(user.userId, newRefreshToken.Token);
            var jwtToken = generateJSONWebToken(user);
            return new AuthenticateResponse(user, jwtToken, refresh_token);
        }

        private string generateJSONWebToken(UserModelGet user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.userId),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(null,
              null,
              claims,
              expires: DateTime.Now.AddMinutes(180),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddYears(1),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
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
