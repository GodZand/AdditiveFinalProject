using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Saintynas.Controllers
{
    public class CategoryController : ApiController
    {
        /// <summary>
        /// Gets all the categories from the api
        /// </summary>
        /// <returns></returns>
        public JArray Get()
        {
            var client = new RestClient("https://vx-e-additives.p.rapidapi.com/categories?sort=name");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "vx-e-additives.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "ce141f8f14mshc3118c3733c840cp12d10bjsn402f62fb4213");
            var response = client.Execute(request).Content;
            dynamic json = JsonConvert.DeserializeObject(response);
            return json;
        }
        /// <summary>
        /// Gets the specified category details
        /// </summary>
        /// <param name="id">The category id number</param>
        /// <returns></returns>
        public JObject Get(int id)
        {
            var client = new RestClient($"https://vx-e-additives.p.rapidapi.com/categories/{id}?locale=en");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "vx-e-additives.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "ce141f8f14mshc3118c3733c840cp12d10bjsn402f62fb4213");
            var response = client.Execute(request).Content;
            dynamic json = JsonConvert.DeserializeObject(response);
            return json;
        }

        
    }
}
