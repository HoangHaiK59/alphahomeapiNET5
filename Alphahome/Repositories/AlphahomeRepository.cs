using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alphahome.Models;
using Alphahome.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySql.Data;
using System.Configuration;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Alphahome.Repositories
{
    public class AlphahomeRepository: IAlphahomeRepositoty
    {
        private string _connectionString { get; set; }

        private IConfiguration _configuration;
        public AlphahomeRepository (IConfiguration configuration)
        {
            //IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //_configuration = builder.Build();
            //_connectionString = _configuration.GetConnectionString("Alphahome");
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Alphahome");
        }

        public Home GetHomePage()
        {
            var procedure = "sp_home";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        var data = reader.Read<ExtractModel>().ToList();
                        var services_types = reader.Read<ServiceType>().ToList();
                        return new Home { services = data, service_types = services_types };
                    }
                    return new Home { services = null, service_types = null };
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        //public ApiResponse GetCategoryList()
        //{
        //    return new ApiResponse();
        //}
        public List<ServiceType> GetServiceType()
        {
            var procedure = "sp_service_type";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    var data = conn.Query<ServiceType>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public List<ServiceHasImages> GetFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            var procedure = "sp_furniture_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("typeId", serviceTypeId);
                    param.Add("offSet", offset);
                    param.Add("pageSize", pagesize);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        var services = reader.Read<ServiceHasImages>().ToList();
                        var images = reader.Read<ImageService>().ToList();
                        foreach(var service in services)
                        {
                            service.images = images.Where(i => i.serviceId == service.id).ToList();
                        }
                        return services;
                    }
                    return new List<ServiceHasImages>();
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<Service> GetWoodenPage(string serviceTypeId, int offset, int pagesize)
        {
            var procedure = "sp_service_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("typeId", serviceTypeId);
                    param.Add("offSet", offset);
                    param.Add("pageSize", pagesize);
                    var data = conn.Query<Service>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<Service> GetContructionFurniturePage(string serviceTypeId, int offset, int pagesize)
        {
            var procedure = "sp_service_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("typeId", serviceTypeId);
                    param.Add("offSet", offset);
                    param.Add("pageSize", pagesize);
                    var data = conn.Query<Service>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public List<Service> GetContructionHousePage(string serviceTypeId, int offset, int pagesize)
        {
            var procedure = "sp_service_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("typeId", serviceTypeId);
                    param.Add("offSet", offset);
                    param.Add("pageSize", pagesize);
                    var data = conn.Query<Service>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Management GetManagerPage(int offset, int pagesize)
        {
            var procedure = "sp_management_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("offSet", offset);
                    param.Add("pageSize", pagesize);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if(reader != null)
                    {
                        var services = reader.Read<Service>().ToList();
                        var projects = reader.Read<Project>().ToList();
                        var posts = reader.Read<Post>().ToList();
                        return new Management { services = services, projects = projects, posts = posts };
                    }
                    return new Management { services = null, projects = null, posts = null };
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ServiceDetail GetDetailById(long sid)
        {
            var procedure = "sp_service_detail";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("sid", sid);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if(reader != null)
                    {
                        var service = reader.Read<ServiceDetail>().FirstOrDefault();
                        var images = reader.Read<Image>().ToList();
                        service.images = images;
                        return service;
                    }

                    return new ServiceDetail();
                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public ServiceDetail GetServiceById(long id)
        {
            var procedure = "sp_manage_service_detail";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        var service = reader.Read<ServiceDetail>().FirstOrDefault();
                        var images = reader.Read<Image>().ToList();
                        service.images = images;
                        return service;
                    }

                    return new ServiceDetail();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public List<Project> GetProjectPage(int offSet, int pageSize)
        {
            var procedure = "sp_project_page";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("offSet", offSet);
                    param.Add("pageSize", pageSize);
                    var data = conn.Query<Project>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ServiceType GetServiceByOrder(int order)
        {
            var procedure = "sp_service_by_order";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("orderParam", order);
                    var data = conn.QueryFirstOrDefault<ServiceType>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public ProjectDetail GetDetailProject(long pId)
        {
            var procedure = "sp_detail_project";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("pId", pId);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if(reader != null)
                    {
                        var project = reader.Read<ProjectDetail>().FirstOrDefault();
                        var images = reader.Read<ImageProject>().ToList();
                        project.images = images;
                        return project;
                    }

                    return new ProjectDetail();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public List<Post> GetPosts(int offSet, int pageSize)
        {
            var procedure = "sp_posts";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("offSet", offSet);
                    param.Add("pageSize", pageSize);
                    var data = conn.Query<Post>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public PostDetail GetDetailPost(long postId)
        {
            var procedure = "sp_detail_post";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("postId", postId);
                    var reader = conn.QueryMultiple(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    if(reader != null)
                    {
                        var post = reader.Read<PostDetail>().FirstOrDefault();
                        post.images = reader.Read<ImageDetail>().ToList();
                        return post;
                    }
                    return new PostDetail();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ResponseInsert RunInsertJson(string jdoc)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"TRUNCATE json_process;insert into json_process values (@jdoc)";
                cmd.Parameters.AddWithValue("@jdoc", jdoc);
                var result = cmd.ExecuteNonQuery();
                var response = new ResponseInsert();
                if (result == 1)
                {
                    response.valid = true;
                }
                else
                {
                    response.valid = false;
                }
                return response;
            }

        }

        public Response UploadLinkAdsVideo(AdsPost adsPost)
        {
            var result = RunInsertJson(adsPost.jdoc);
            if (result.valid)
            {
                var procedure = "sp_ads_video_set";

                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public Response DeleteAdsVideo(int id)
        {
            var procedure = "sp_ads_delete";

            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Ads FindAdsById(int id)
        {
            var procedure = "sp_get_ads_by_id";

            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var data = conn.QueryFirstOrDefault<Ads>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Response UpdateQueue(Ads ads)
        {
            var procedure = "sp_ads_update_queue";

            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("id", ads.id);
                    param.Add("queue", ads.queue);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Response SetNewPost(PostParam post)
        {

            var result = RunInsertJson(post.images);

            if (result.valid)
            {
                var procedure = "sp_post_set";

                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INname", post.name);
                        param.Add("INcontent", post.content);
                        param.Add("INurl", post.url);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            } else
            {
                return null;
            }

        }

        public Response SetNewService(ServicePost service)
        {
            var result = RunInsertJson(service.images);
            if (result.valid)
            {
                var procedure = "sp_service_set";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INname", service.name);
                        param.Add("INdescription", service.description);
                        param.Add("INcontent", service.content);
                        param.Add("INurl", service.url);
                        param.Add("INserviceTypeId", service.serviceTypeId);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            } else
            {
                return null;
            }
        }
        public Response SetNewProject(ProjectPost project)
        {
            var result = RunInsertJson(project.images);
            if (result.valid)
            {
                var procedure = "sp_project_set";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INname", project.name);
                        param.Add("INdescription", project.description);
                        param.Add("INcontent", project.content);
                        param.Add("INurl", project.url);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            } else
            {
                return null;
            }

        }
        public List<ManageService> GetServices(int offSet, int pageSize)
        {
            var procedure = "sp_services";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("offSet", offSet);
                    param.Add("pageSize", pageSize);
                    var data = conn.Query<ManageService>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<string> UploadImage(IFormFile uFile)
        {
            if (uFile != null && uFile.Length > 0)
            {
                // var fileName = Path.GetFileName(uFile.FileName);
                var fileName = Guid.NewGuid().ToString();
                fileName = fileName + Path.GetExtension(uFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uFile.CopyToAsync(fileStream);
                }
                return "/images/" + fileName;
            }
            return string.Empty;
        }

        public async Task<List<string>> UploadMultiImage(IFormFileCollection formFiles)
        {
            List<string> fileCreates = new List<string>();
            if (formFiles != null && formFiles.Count > 0)
            {
               foreach(var file in formFiles)
                {
                    var fileName = Guid.NewGuid().ToString();
                    fileName = fileName + Path.GetExtension(file.FileName);
                    var fileUrl = "/images/" + fileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if (fileName != "")
                    {
                        fileCreates.Add(fileUrl);
                    }
                }

            }
            return fileCreates;
        }
        public Task<Response> FindUserAsync(string userId)
        {
            var procedure = "sp_find_user_by_userId";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("userId", userId);
                    var data = conn.QueryFirstOrDefaultAsync<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<List<Ads>> UploadVideoAds(IFormFileCollection formFiles)
        {
            List<Ads> fileCreates = new List<Ads>();
            if (formFiles != null && formFiles.Count > 0)
            {
                foreach (var file in formFiles)
                {
                    var fileName = Guid.NewGuid().ToString();
                    fileName = fileName + Path.GetExtension(file.FileName);
                    var fileUrl = "/ads/" + fileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ads", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if (fileName != "")
                    {
                        var fileDoneUpload = new Ads { name = fileName, url = fileUrl };
                        fileCreates.Add(fileDoneUpload);
                    }
                }

            }
            return fileCreates;
        }
        public List<Ads> GetAdsVideo(int numVideo)
        {
            var procedure = "sp_ads_video";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("numVideo", numVideo);
                    var data = conn.Query<Ads>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public List<Ads> ManageAdsVideo()
        {
            var procedure = "sp_manage_ads_video";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    var data = conn.Query<Ads>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Response DeleteService(ServiceDelete serviceDelete)
        {
            var procedure = "sp_del_service";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("sId", serviceDelete.id);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Response DeleteProject(ProjectDelete projectDelete)
        {
            var procedure = "sp_del_project";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("pId", projectDelete);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public Response DeletePost(PostDelete postDelete)
        {
            var procedure = "sp_del_post";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("mpostId", postDelete.id);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public UserModelGet Authenticate(AlphahomeUser user)
        {
            var procedure = "sp_user_login";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("email", user.email);
                    param.Add("password", user.password);
                    var data = conn.QueryFirstOrDefault<UserModelGet>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Response UpdateUserToken(string userId, string refresh_token)
        {
            var procedure = "sp_user_update_token";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("userId", userId);
                    param.Add("refresh_token", refresh_token);
                    var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public UserModelGet FindUserByRefreshToken(string refresh_token)
        {
            var procedure = "sp_user_find_by_token";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("refresh_token", refresh_token);
                    var data = conn.QueryFirstOrDefault<UserModelGet>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Response UpdatePost(PostUpdate post)
        {
            var result = RunInsertJson(post.images);

            if (result.valid)
            {
                var procedure = "sp_post_update";

                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INid", post.id);
                        param.Add("INname", post.name);
                        param.Add("INcontent", post.content);
                        param.Add("INurl", post.url);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return null;
            }

        }
        public Response UpdateService(ServiceUpdate service)
        {
            var result = RunInsertJson(service.images);
            if (result.valid)
            {
                var procedure = "sp_service_update";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INid", service.id);
                        param.Add("INname", service.name);
                        param.Add("INdescription", service.description);
                        param.Add("INcontent", service.content);
                        param.Add("INurl", service.url);
                        param.Add("INserviceTypeId", service.serviceTypeId);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return null;
            }
        }
        public Response UpdateProject(ProjectUpdate project)
        {
            var result = RunInsertJson(project.images);
            if (result.valid)
            {
                var procedure = "sp_project_update";
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        var param = new DynamicParameters();
                        param.Add("INid", project.id);
                        param.Add("INname", project.name);
                        param.Add("INdescription", project.description);
                        param.Add("INcontent", project.content);
                        param.Add("INurl", project.url);
                        var data = conn.QueryFirstOrDefault<Response>(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
                        return data;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public List<Search> Search(string query)
        {
            var procedure = "sp_search";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("q", query);
                    var data = conn.Query<Search>(procedure, param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    return data;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
