﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CreativeGurus.Weather.Wunderground.Utilities
{
    internal static class RestRequest
    {
        internal static T Execute<T>(Uri uri) where T : new()
        {
            IRestRequest request = new RestSharp.RestRequest();

            var client = new RestClient();
            client.BaseUrl = uri;
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                if (response.Content.Length > 0)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content, new BoolConverter(), new DoubleConverter());
                }
                else
                {
                    throw response.ErrorException;
                }
            }

            return response.Data;
        }

        internal async static Task<T> ExecuteAsync<T>(Uri uri) where T : new()
        {
            T result = new T();

            IRestRequest request = new RestSharp.RestRequest();

            var client = new RestClient();
            client.BaseUrl = uri;

            var response = await client.ExecuteGetTaskAsync<T>(request).ConfigureAwait(false);

            if (response.ErrorException != null)
            {
                if (response.Content.Length > 0)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content, new BoolConverter(), new DoubleConverter());
                }
                else
                {
                    throw response.ErrorException;
                }
            }

            return response.Data;
        }
    }
}