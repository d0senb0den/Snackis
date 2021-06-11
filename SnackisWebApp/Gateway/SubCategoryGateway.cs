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
    public class SubCategoryGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public SubCategoryGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/SubCategories");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SubCategory>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<SubCategory>>(_configuration["SnackisAPI"] + "/SubCategories");
        }

        public async Task<SubCategory> PostSubCategories(SubCategory subCategory)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/SubCategories", subCategory);
            SubCategory returnValue = await response.Content.ReadFromJsonAsync<SubCategory>();
            return returnValue;
        }
        public async Task<SubCategory> DeleteSubCategories(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/SubCategories/" + deleteId);
            SubCategory returnValue = await response.Content.ReadFromJsonAsync<SubCategory>();
            return returnValue;
        }

        public async Task PutSubCategories(Guid editId, SubCategory subCategory)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/SubCategories/" + editId, subCategory);
        }
    }
}
