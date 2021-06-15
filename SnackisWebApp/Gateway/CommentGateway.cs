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
    public class CommentGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CommentGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Comment>> GetComments()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/Comments");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comment>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<Comment>>(_configuration["SnackisAPI"] + "/Comments");
        }

        public async Task<Comment> PostComments(Comment comment)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/Comments", comment);
            Comment returnValue = await response.Content.ReadFromJsonAsync<Comment>();
            return returnValue;
        }
        public async Task<bool> DeleteComments(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/Comments/" + deleteId);
            return response.IsSuccessStatusCode;
        }

        public async Task PutComments(Guid editId, Comment comment)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/Comments/" + editId, comment);
        }
    }
}
