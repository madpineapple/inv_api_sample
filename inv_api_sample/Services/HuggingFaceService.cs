using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

public class HuggingFaceService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "api string";
    private readonly string _modelName = "model name";

    public HuggingFaceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",$"Bearer {_apiKey}");
    }

    public async Task<string> QueryAsync(string input)
    {
        var payload = new { inputs = input };
        var json = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"https://api-inference.huggingface.co/models/{_modelName}", json);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"HuggingFace error: {response.StatusCode}\n{result}");

        return result;
    }
}
