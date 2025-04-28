using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class LLMService
{
    private readonly HttpClient _httpClient;
    private readonly string _llmApiUrl = "http://192.168.0.195:11434/api/generate"; 
    private readonly string _systemPrompt =
     @"You are a helpful,
     respectful, and honest warehouse assistant. Provide accurate, concise, and polite answers. 
     If you don’t know the answer, say so and suggest where to find more information.";
     private readonly List<(string Role, string Content)> _chatHistory;
private readonly string _inventoryApiUrl = "<api url>"; // Update as needed

    public LLMService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _chatHistory = new List<(string Role,string Content)>();   
    }

    public async Task<string> GetModelResponse(string inputText)
    {
      var inventoryPatterns = new List<Regex>
    {
        new Regex(@"(?i)how\s+much\s+([a-z\s]+)\s+(is\s+in\s+stock|do\s+we\s+have|is\s+available|are\s+there|is\s+left)\?", RegexOptions.IgnoreCase),
        new Regex(@"(?i)how\s+many\s+([a-z\s]+)\s+(do\s+we\s+have|are\s+there|is\s+in\s+stock)\?", RegexOptions.IgnoreCase),
        new Regex(@"(?i)what\s+(is|are)\s+the\s+stock\s+of\s+([a-z\s]+)\?", RegexOptions.IgnoreCase),
        new Regex(@"(?i)how\s+many\s+units\s+of\s+([a-z\s]+)\s+do\s+we\s+have\?", RegexOptions.IgnoreCase)
    };

    foreach (var pattern in inventoryPatterns)
    {
        var match = pattern.Match(inputText);
        if (match.Success)
        {
            string itemName = match.Groups[1].Value.Trim();
            try
            {
            // Call inventory API
            HttpResponseMessage response = await _httpClient.GetAsync($"{ _inventoryApiUrl }/{Uri.EscapeDataString(itemName)}");
            if (response.IsSuccessStatusCode)
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
                _chatHistory.Add(("user", inputText));
                _chatHistory.Add(("assistant", result));
                
                return result;
            }
            else
            {
                string error = "Sorry, I couldn’t find the item quantity. Please try again.";
                _chatHistory.Add(("user", inputText));
                _chatHistory.Add(("assistant", error));
                return error;
            }
            }
             catch (Exception ex)
                {
                    string error = $"Error accessing inventory: {ex.Message}";
                    _chatHistory.Add(("user", inputText));
                    _chatHistory.Add(("assistant", error));
                    return error;
                }
        }
    }

        // Add user input to chat history
        _chatHistory.Add(("user", inputText));

        // Build the prompt using the chat template
        var promptBuilder = new StringBuilder();
        
        // Add system prompt
        promptBuilder.AppendLine($"<|im_start|>system");
        promptBuilder.AppendLine(_systemPrompt);
        promptBuilder.AppendLine($"<|im_end|>");

        // Add chat history (limited to last few messages to avoid token limits)
        int maxHistory = 4; // Adjust based on context length (e.g., 2048 tokens)
        int startIndex = Math.Max(0, _chatHistory.Count - maxHistory);
        for (int i = startIndex; i < _chatHistory.Count; i++)
        {
            var (role, content) = _chatHistory[i];
            promptBuilder.AppendLine($"<|im_start|>{role}");
            promptBuilder.AppendLine(content);
            promptBuilder.AppendLine($"<|im_end|>");
        }

        // Add assistant role start
        promptBuilder.AppendLine($"<|im_start|>assistant");

        string fullPrompt = promptBuilder.ToString();

        // Create request body
        var requestBody = new { model = "tinyllama:latest", prompt = fullPrompt };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

       try
       {
        HttpResponseMessage response = await _httpClient.PostAsync(_llmApiUrl, jsonContent);
    
        if(response.IsSuccessStatusCode)
         {
            var rawJson = await response.Content.ReadAsStringAsync();
         
              var resultBuilder = new StringBuilder();
            foreach (string line in rawJson.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    var lineJson = JsonConvert.DeserializeObject<dynamic>(line);
                    resultBuilder.Append(lineJson.response.ToString());
                }
                catch (JsonReaderException)
                {
                    // Ignore malformed lines
                }
            }
       string finalResponse = resultBuilder.ToString();

                // Add assistant response to chat history
                _chatHistory.Add(("assistant", finalResponse));
            return resultBuilder.ToString();
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
