using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ReactDonationApplication.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ReactDonationApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ProjectsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult GetAllProjects()
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.GET_ALL_PROJECTS", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                dt.Load(cmd.ExecuteReader());
                connection.Close();
            }

            return new JsonResult(dt);
        }

        [HttpPost]
        [HttpPut]        
        public JsonResult PostPutProjects(ProjectsModel project)
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.INSERT_OR_UPDATE_PROJECT", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", project.Id);
                cmd.Parameters.AddWithValue("@PROJECT_NAME", project.PROJECT_NAME);
                cmd.Parameters.AddWithValue("@PROJECT_DESCRIPTION", project.PROJECT_DESCRIPTION);
                cmd.Parameters.AddWithValue("@PROJECT_CODE", project.PROJECT_CODE);
                cmd.Parameters.AddWithValue("@PROJECT_ORGANIZATION_CODE", project.PROJECT_ORGANIZATION_CODE);
                cmd.Parameters.AddWithValue("@PROJECT_FUND", project.PROJECT_FUND);
                cmd.Parameters.AddWithValue("@PROJECT_TARGET_FUND", project.PROJECT_TARGET_FUND);

                cmd.ExecuteNonQuery();
                connection.Close();
            }

            if (project.Id == 0)
                return new JsonResult("Added Successfully");
            else return new JsonResult("Updated Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (System.Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
