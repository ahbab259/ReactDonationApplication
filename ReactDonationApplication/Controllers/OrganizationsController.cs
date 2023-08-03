using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactDonationApplication.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ReactDonationApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrganizationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet] 
        public JsonResult GetOrganizations()
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.GET_ALL_ORGANIZATIONS", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                connection.Close();
            }

            return new JsonResult(dt);
        }

        [HttpGet("countrycode/{countryCode}")]
        public JsonResult GetOrganizationsByCountryCode(string countryCode)
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.GET_ORGANIZATIONS_BY_COUNTRY_CODE", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrganizationCountryCode", countryCode);
                dt.Load(cmd.ExecuteReader());
                connection.Close();
            }

            return new JsonResult(dt);
        }
        //[FromRoute(Name = "organizationDetails")] 
        [HttpGet("organizationDetails/{orgId}")]
        public JsonResult GetOrganizationDetails(int orgId)
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.GET_ORGANIZATION_BY_ID", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", orgId);
                dt.Load(cmd.ExecuteReader());
                connection.Close();
            }

            return new JsonResult(dt);
        }

        [HttpPost]
        [HttpPut]
        public JsonResult PostPutOrganizations(OrganizationsModel org)
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.INSERT_OR_UPDATE_ORGANIZATION", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", org.Id);
                cmd.Parameters.AddWithValue("@OrganizationName", org.OrganizationName);
                cmd.Parameters.AddWithValue("@OrganizationCode", org.OrganizationCode);
                cmd.Parameters.AddWithValue("@OrganizationType", org.OrganizationType);
                cmd.Parameters.AddWithValue("@OrganizationDescription", org.OrganizationDescription);
                cmd.Parameters.AddWithValue("@OrganizationCountryCode", org.OrganizationCountryCode);
                cmd.Parameters.AddWithValue("@OrganizationCountryName", org.OrganizationCountryName);
                cmd.Parameters.AddWithValue("@OrganizationEmail", org.OrganizationEmail);
                cmd.Parameters.AddWithValue("@OrganizationPhone", org.OrganizationPhone);

                cmd.ExecuteNonQuery();
                connection.Close();
            }

            if(org.Id == 0)
            return new JsonResult("Added Successfully");
            else return new JsonResult("Updated Successfully");
        }

        [HttpDelete("deleteOrganization/{id}")]
        public JsonResult DeleteOrganizations(int id)
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("dbo.DELETE_ORGANIZATION", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
