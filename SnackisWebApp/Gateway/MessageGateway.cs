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
    public class MessageGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public MessageGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Message>> GetMessages()
        {
            /*var response = await _httpClient.GetAsync(_configuration["SnackisAPI"] + "/Messages");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Message>>(apiResponse);*/

            return await _httpClient.GetFromJsonAsync<List<Message>>(_configuration["SnackisAPI"] + "/Messages");
        }

        public async Task<Message> PostMessages(Message message)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["SnackisAPI"] + "/Messages", message);
            Message returnValue = await response.Content.ReadFromJsonAsync<Message>();
            return returnValue;
        }
        public async Task<Message> DeleteMessages(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["SnackisAPI"] + "/Messages/" + deleteId);
            Message returnValue = await response.Content.ReadFromJsonAsync<Message>();
            return returnValue;
        }

        public async Task PutMessages(Guid editId, Message message)
        {
            await _httpClient.PutAsJsonAsync(_configuration["SnackisAPI"] + "/Messages/" + editId, message);
        }
    }
}
