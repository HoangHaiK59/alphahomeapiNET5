﻿using System;
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
        //public ApiResponse GetHomePage()
        //{
        //    return new ApiResponse();

        //}
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
                        return new Management { services = services, projects = projects };
                    }
                    return new Management { services = null, projects = null };
                    //return new ApiResponse("success", data, 200, "1.0.0.0");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ServiceDetail GetDetailById(long sid, string serviceTypeId)
        {
            var procedure = "sp_service_detail";
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("sid", sid);
                    param.Add("stypeId", serviceTypeId);
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
                        param.Add("name", service.name);
                        param.Add("description", service.description);
                        param.Add("content", service.content);
                        param.Add("url", service.url);
                        param.Add("serviceTypeId", service.serviceTypeId);
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
                fileName = fileName + "." + Path.GetFileName(uFile.FileName).Split(".")[1];
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
                    fileName = fileName + "." + Path.GetFileName(file.FileName).Split(".")[1];
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
    }
}
