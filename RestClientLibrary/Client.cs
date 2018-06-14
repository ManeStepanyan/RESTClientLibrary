using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RestClientLibrary
{
    /// <summary>
    /// Universal library to work on any Web API
    /// </summary>
    /// <typeparam name="T">Type of my model</typeparam>
    public class Client<T> : IClient<T> where T : class, new()
    {
        /// <summary>
        /// application/json or application/xml
        /// </summary>
        readonly string contentType;

        /// <summary>
        /// HTTP Client
        /// </summary>
        readonly HttpClient client;

        /// <summary>
        /// Instance of serializer
        /// </summary>
        readonly Serializer ser;

        /// <summary>
        /// URL of Web API
        /// </summary>
        readonly string url;

        /// <summary>
        /// Additional parameters like api/Products
        /// </summary>
        public string parameters = "";

        /// <summary>
        /// Creating a client
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        public Client(string url, string contentType)
        {
            this.client = new HttpClient();
            this.url = url;
            this.contentType = contentType;
            this.ser = new Serializer();
            CreateRequest();
        }
        /// <summary>
        /// Creating request
        /// </summary>
        private void CreateRequest()
        {
            this.client.BaseAddress = new Uri(this.url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.contentType));
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <returns>List of items</returns>
        public List<T> Get()
        {
            List<T> result = new List<T>();
            var response = client.GetAsync(url + parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                //For example if we have parameters like api/Products/1 there is no need to return list
                if (parameters.Split('/').Length > 2)
                {
                    result.Add(ser.DeserializeToObject<T>(response.Content.ReadAsStringAsync().Result, contentType));
                }
                else result = ser.Deserialize<T>(response.Content.ReadAsStringAsync().Result, contentType);
            }
            return result;
        }

        /// <summary>
        /// Deleting item
        /// </summary>

        public void Delete()
        {
            var response = client.DeleteAsync(url + parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Succesfully deleted");
                Console.WriteLine(response);
            }
        }

        /// <summary>
        /// Adding new item
        /// </summary>
        /// <param name="obj">New item</param>

        public void Post(T obj)
        {
            var response = client.PostAsync(url + parameters, new StringContent(ser.Serialize<T>(obj, contentType), Encoding.UTF8, contentType)).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Posted");
                Console.WriteLine(content);
            }
        }

        /// <summary>
        /// Updating item
        /// </summary>
        /// <param name="obj">Updated item</param>
        public void Put(T obj)
        {
            HttpResponseMessage response = client.PutAsync(url + parameters, new StringContent(ser.Serialize<T>(obj, contentType), Encoding.UTF8, contentType)).Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Put");
                Console.WriteLine(res);
            }
        }
    }
}
