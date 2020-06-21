using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Gherkin.Ast;
using Saintynas.Models;

namespace Saintynas.Controllers
{   // Path: api/Test
    public class AdditivesController : ApiController
    {
        public static string connectionstring ="datasource=127.0.0.1;port=3306;username=root;password=";
        /// <summary>
        /// Gets all the the additives from api
        /// </summary>
        /// <returns></returns>
        public JArray Get()
        {
           
                var client = new RestClient("https://vx-e-additives.p.rapidapi.com/additives?order=asc&locale=en&sort=last_update");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "vx-e-additives.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "ce141f8f14mshc3118c3733c840cp12d10bjsn402f62fb4213");
                var response = client.Execute(request).Content;
                dynamic json = JsonConvert.DeserializeObject(response);
                return json;
           
        }
        /// <summary>
        /// Gets the specified additive from api
        /// </summary>
        /// <param name="name">Name of the additive</param>
        /// <returns></returns>
        public JArray Get(string name)
        {
           
                var client = new RestClient($"https://vx-e-additives.p.rapidapi.com/additives/search?sort=name&q={name}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "vx-e-additives.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "ce141f8f14mshc3118c3733c840cp12d10bjsn402f62fb4213");
                var response = client.Execute(request).Content;
                dynamic json = JsonConvert.DeserializeObject(response);
                return json;
            
        }
        /// <summary>
        /// Gets the specified additive with more details from api
        /// </summary>
        /// <param name="code">The additive code number</param>
        /// <returns></returns>
        public JObject Get(int code)
        {

            var client = new RestClient($"https://vx-e-additives.p.rapidapi.com/additives/{code}?locale=en");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "vx-e-additives.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "ce141f8f14mshc3118c3733c840cp12d10bjsn402f62fb4213");
            var response = client.Execute(request).Content;
            dynamic json = JsonConvert.DeserializeObject(response);
            return json;

        }

        /// <summary>
        /// Adds selected additive name to the database
        /// </summary>
        /// <param name="Name">Name of additive</param>
        [Route("api/Additives/Insert/DB")]
        [HttpPost]
        public void Post(string Name)
        {

            using (MySqlConnection dbconnect = new MySqlConnection(connectionstring))
            {


                string mysql = $"insert into finalproject.additive (Name) VALUES (@Name)";
            
            MySqlCommand command = new MySqlCommand(mysql, dbconnect);
            command.Parameters.AddWithValue("@Name", Name);
                dbconnect.Open();
            command.ExecuteNonQuery();
                dbconnect.Close();
            }


        }
        /// <summary>
        /// Deletes the specified additive from database
        /// </summary>
        /// <param name="Name">Name of additive</param>
        [Route("api/Additives/Delete/DB")]
        [HttpDelete]
        public void Delete(string Name)
        {

            using (MySqlConnection dbconnect = new MySqlConnection(connectionstring))
            {


                string mysql = $"Delete from finalproject.additive where Name=@Name";

                MySqlCommand command = new MySqlCommand(mysql, dbconnect);
                command.Parameters.AddWithValue("@Name", Name);
                dbconnect.Open();
                command.ExecuteNonQuery();
                dbconnect.Close();
            }


        }

        /// <summary>
        /// Gets all the additive names from the database
        /// </summary>
        /// <returns></returns>
        [Route("api/Additives/Get/DB")]
        [HttpGet]
        public List<AdditivesList> Getall()
        {
            List<AdditivesList> Names = new List<AdditivesList>();
            string mysql = $"select DISTINCT Name from finalproject.additive";
            using (MySqlConnection dbconnect = new MySqlConnection(connectionstring))
            {
              
                dbconnect.Open();
                using (MySqlCommand command = new MySqlCommand(mysql, dbconnect))
                {
                    MySqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        AdditivesList lis = new AdditivesList();
                        lis.SetName(Convert.ToString(read[0]));
                        Names.Add(lis);

                    }
                }
                dbconnect.Close();
            }
            return Names;

        }
    }





}
