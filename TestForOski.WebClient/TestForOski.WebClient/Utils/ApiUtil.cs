using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestForOski.WebClient.Exceptions;
using TestForOski.WebClient.Models.Account;
using TestForOski.WebClient.Utils.Interfaces;

namespace TestForOski.WebClient.Utils
{
    public class ApiUtil : IApiUtil
    {
        private IHttpUtil _httpUtil;

        public ApiUtil(IHttpUtil httpUtil)
        {
            _httpUtil = httpUtil;
        }

        private async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage httpResponse)
        {
            string message = await _httpUtil.EnsureSuccessStatusCode(httpResponse);
            if (!string.IsNullOrEmpty(message))
            {
                throw new HttpStatusException(httpResponse.StatusCode, message);
            }
        }

        public async Task<string> SignInAsync(string url, AuthenticationDto authModel)
        {
            var httpResponse = await _httpUtil.PostJsonAsync(url, authModel);

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            httpResponse.Headers.TryGetValues("Authorization", out IEnumerable<string> token);

            return token.FirstOrDefault();
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var httpResponse = await _httpUtil.GetAsync(url);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);


            return responseModel;
        }

        public async Task<TResponse> CreateAsync<TResponse>(string url, TResponse data)
        {
            var httpResponse = await _httpUtil.PostJsonAsync(url, data);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }

        public async Task<TResponse> CreateAsync<TResponse, TRequest>(string url, TRequest data)
        {
            var httpResponse = await _httpUtil.PostJsonAsync(url, data);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string url, TRequest data)
        {
            var httpResponse = await _httpUtil.PostJsonAsync(url, data);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }

        public async Task<TResponse> PutAsync<TResponse>(string url, TResponse data)
        {
            var httpResponse = await _httpUtil.PutJsonAsync(url, data);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }

        public async Task<TResponse> PutAsync<TResponse, TRequest>(string url, TRequest data)
        {
            var httpResponse = await _httpUtil.PutJsonAsync(url, data);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }


        public async Task<TResponse> DeleteAsync<TResponse>(string url)
        {
            var httpResponse = await _httpUtil.DeleteAsync(url);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);

            return responseModel;
        }

        public async Task<TResponse> EnableAsync<TResponse>(string url)
        {
            var httpResponse = await _httpUtil.PatchAsync(url);

            await EnsureSuccessStatusCodeAsync(httpResponse);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(stringResponse);
            
            return responseModel;
        }
    }
}
