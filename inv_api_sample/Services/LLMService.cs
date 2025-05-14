using System.Text;
using Newtonsoft.Json;


public class LLMService
{
    private readonly HttpClient _httpClient;

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _localApiUrl = "http://localhost:8000/parse-intent"; // Replace with your FastAPI endpoint

    private readonly string _inventoryApiUrl ="http://localhost:5230/Product/info";

    public class OuterResponse
{
    public string? Response { get; set; }
}

public class IntentPayload
{
    public string? Intent { get; set; }
    public string? Item { get; set; }
}

    public LLMService(IHttpClientFactory httpClientFactory, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClientFactory = httpClientFactory;
    }

 public async Task<string> HandleUserRequest(string userInput)
{
    string aiResponse = await QueryLocalApiAsync(userInput);
    ParsedIntent intentData;
    
    try
    {
        intentData =  JsonConvert.DeserializeObject<ParsedIntent>(aiResponse);
    }
     catch (JsonException ex)
    {
        return $"Tiny returned invalid JSON: {ex.Message}";
    }
if (intentData == null || string.IsNullOrWhiteSpace(intentData.Intent))
        return "Tiny didn't return a valid intent.";

    var intent = intentData.Intent;
    var item = intentData.Item;

    if (intent == "read_item" && !string.IsNullOrWhiteSpace(item))
    {
        var stockData = await GetStockDataAsync(item);
        return stockData;
    }

    return "I'm sorry, I couldn't understand your request.";
}
       // Step 1: Call your local API for TinyLlama's response
    private async Task<string> QueryLocalApiAsync(string userInput)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestBody = new
        {
            prompt = userInput,
            max_new_tokens = 100 // You can adjust the parameters if needed
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(_localApiUrl, content);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error calling local API: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }
 private async Task<string> GetStockDataAsync(string itemName)
 {
    try
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ _inventoryApiUrl }/{Uri.EscapeDataString(itemName)}");
         if(response.IsSuccessStatusCode)
         {
            var inventoryData = JsonConvert.DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());
                 // If there's more than one item, summarize the total quantity
                float totalQuantity = inventoryData.Sum(item => item.ProdQuantity);

                string result = $"We have {totalQuantity} units of {itemName}. Here are the details:\n";
                foreach (var item in inventoryData)
                {
                    result += $"{item.ProdItemName} - {item.ProdQuantity} units at {item.ProdItemLoc}\n";
                }
                
                // Add to chat history
                // _chatHistory.Add(("user", inputText));
                // _chatHistory.Add(("assistant", result));
                
                return result;
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
