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
    public class CategoryGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CategoryGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/Categories");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Category>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<Category>>(_configuration["SnackisAPI"] + "/Categories");
        }

        public async Task<Category> PostCategories(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/Categories", category);
            Category returnValue = await response.Content.ReadFromJsonAsync<Category>();
            return returnValue;

            /*return await _httpClient.PostAsJsonAsync<Category>(_configuration["SnackisAPI"] + "/Categories", category);*/
        }
        public async Task DeleteCategories(Guid deleteId)
        {
            await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/Categories/" + deleteId);
            /*Category returnValue = await response.Content.ReadFromJsonAsync<Category>();
            return returnValue;*/
        }

        public async Task PutCategories(Guid editId, Category category)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/Categories/" + editId, category);
        }

    }
}
