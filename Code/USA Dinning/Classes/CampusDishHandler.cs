using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace CampusDish
{
    public static class CampusDishHandler
    {
        /// <summary>
        /// The default URL for your school
        /// </summary>
        public static string DefaultURL { get; set; }

        /// <summary>
        /// Gets the menu for a specified day.
        /// </summary>
        /// <param name="date">The date to get</param>
        /// <param name="time">Unknown</param>
        /// <param name="periodId">Unknown</param>
        /// <param name="storeIds">Unknown</param>
        /// <returns></returns>
        public static async Task<MenuJsonRoot> GetDailyMenu(string locationId, string date, string time="", string periodId="", string storeIds = "")
        {
            // The rest client with the URL to request from
            RestClient client = new RestClient($"{DefaultURL}/api/menu/GetMenus?");
            
            // Build the request
            RestRequest request = new RestRequest();
            request.Method = Method.Get;
            request.AddQueryParameter("locationId", locationId);
            request.AddQueryParameter("mode", "Daily");
            request.AddQueryParameter("date", date);
            request.AddQueryParameter("time", time);
            request.AddQueryParameter("periodId", periodId);
            request.AddQueryParameter("storeIds", storeIds);
            request.AddQueryParameter("fulfillmentMethod", "");

            // Send the request
            var response = await client.GetAsync(request);

            // Deserialize Json
            MenuJsonRoot json = null;
            try
            {
                json = JsonConvert.DeserializeObject<MenuJsonRoot>(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return json;
        }
        public static async Task<ValidDatesJson> GetValidDates(string locationId)
        {
            // The rest client with the URL to request from
            RestClient client = new RestClient($"{DefaultURL}/api/menus/GetMenuCalendar?");
            
            // Build the request
            RestRequest request = new RestRequest();
            request.Method = Method.Get;
            request.AddQueryParameter("locationId", locationId);

            // Send the request
            var response = await client.GetAsync(request);

            // Deserialize Json
            ValidDatesJson json = null;
            try
            {
                json = JsonConvert.DeserializeObject<ValidDatesJson>(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return json;
        }
    }
}
