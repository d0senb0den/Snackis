using Microsoft.Extensions.Configuration;
using SnackisWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnackisWebApp.Gateway
{
    public class ReportGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ReportGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Report>> GetReports()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/Reports");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Report>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<Report>>(_configuration["SnackisAPI"] + "/Reports");
        }

        public async Task<Report> PostReports(Report report)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/Reports", report);
            Report returnValue = await response.Content.ReadFromJsonAsync<Report>();
            return returnValue;
        }
        public async Task<Report> DeleteReports(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/Reports/" + deleteId);
            Report returnValue = await response.Content.ReadFromJsonAsync<Report>();
            return returnValue;
        }

        public async Task PutReports(Guid editId, Report report)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/Reports/" + editId, report);
        }
    }
}
