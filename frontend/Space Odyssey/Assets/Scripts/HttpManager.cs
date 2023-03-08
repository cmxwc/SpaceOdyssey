using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

public class HttpManager
{
    public static string http_url = "http://localhost:8000/";

    public static TResultType Get<TResultType>(string url)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Debug.Log(responseStr);
        var result = JsonConvert.DeserializeObject<TResultType>(responseStr);
        return result;
    }

    public static string Task(string url)
    {
        // using (var client = new HttpClient())
        // {
        //     var response = await client.GetAsync(url);
        //     response.EnsureSuccessStatusCode();
        //     return await response.Content.ReadAsStringAsync();
        // }
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }

    public static string Post<TPostType>(string url, TPostType obj)
    {
        HttpClient client = new HttpClient();
        var jsonString = JsonConvert.SerializeObject(obj);
        Debug.Log(jsonString);
        var formContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync(url, formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }

    public static string Put<TPutType>(string url, TPutType obj)
    {
        HttpClient client = new HttpClient();
        var formContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PutAsync(url, formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }

    public static string Delete(string url)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.DeleteAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }
}