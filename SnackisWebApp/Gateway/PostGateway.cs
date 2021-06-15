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
    public class PostGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public PostGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPosts()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/Posts");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Post>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<Post>>(_configuration["SnackisAPI"] + "/Posts");
        }
        public async Task<Post> GetPost(Guid postId)
        {
            return await _httpClient.GetFromJsonAsync<Post>(_configuration["SnackisAPI"] + "/Posts/" + postId);
        }

        public async Task<bool> PostPosts(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/Posts", post);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeletePosts(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/Posts/" + deleteId);
            return response.IsSuccessStatusCode;
        }

        public async Task PutPosts(Guid editId, Post post)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/Posts/" + editId, post);
        }
    }
}
