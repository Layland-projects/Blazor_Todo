using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using Todo_Service.Models;
using Todo_Service.ApiHelpers.Interfaces;
using System.Threading.Tasks;

namespace Todo_Service.ApiHelpers
{
    public class TodoApiHelper : ITodoApiHelper
    {
        IRestClient _restClient;
        IRestRequest _restRequest;
        const string _baseUri = "https://localhost:44368/";
        string _requestUri = "";
        KeyValuePair<string, string> _contentType;
        public TodoApiHelper(IRestClient client, string contentType = "application/json")
        {
            _restClient = client;
            _contentType = new KeyValuePair<string, string>("Content-Type", contentType);
        }

        public async Task<IList<TodoItem>> GetTodoItems(int id = 0, int timeout = 5)
        {
            _restRequest = new RestRequest();
            _restRequest.AddHeader(_contentType.Key, _contentType.Value);
            _requestUri = _baseUri + "api/TodoItems";
            _restRequest.Method = Method.GET;
            _restRequest.Timeout = timeout;
            _restRequest.Resource = _requestUri;
            var res = _restClient.Execute(_restRequest);
            return JsonConvert.DeserializeObject<IList<TodoItem>>(res.Content);
        }

        public async Task<string> PostTodoItem(TodoItem item, int timeout = -1)
        {
            _restRequest = new RestRequest();
            _requestUri = _baseUri + "api/TodoItems";
            _restRequest.Method = Method.POST;
            _restRequest.AddJsonBody(JsonConvert.SerializeObject(item));
            _restRequest.Timeout = timeout;
            _restRequest.Resource = _requestUri;
            var response = await _restClient.ExecuteAsync(_restRequest);
            return response.StatusCode.ToString();
        }

        public async Task<string> DeleteToDoItem(TodoItem item, int timeout = -1)
        {
            _restRequest = new RestRequest();
            _requestUri = _baseUri + $"api/TodoItems/{item.Id}";
            _restRequest.Method = Method.DELETE;
            _restRequest.Timeout = timeout;
            _restRequest.Resource = _requestUri;
            var response = await _restClient.ExecuteAsync(_restRequest);
            return response.StatusCode.ToString();
        }
    }
}
