using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class LLMService
{
    private readonly HttpClient _httpClient;
    private readonly string _llmApiUrl = ""; // Change to match your LLM API

    public LLMService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetModelResponse(string inputText)
    {
        var requestBody = new { prompt = inputText };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

       try
       {
        HttpResponseMessage response = await _httpClient.PostAsync(_llmApiUrl, jsonContent);
    
        if(response.IsSuccessStatusCode)
         {
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent; // This should be the LLM response
         }
           else
            {
                // Handle unsuccessful responses
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Details: {errorContent}");
                return null;
            }
       }
       catch (Exception ex)
       { 
        Console.WriteLine(ex.Message);
        return null;
       }
        

       
    }
}
