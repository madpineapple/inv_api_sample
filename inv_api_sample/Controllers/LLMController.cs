using Microsoft.AspNetCore.Mvc;
namespace inv_api_sample.Controllers
{
[ApiController]
[Route("api/llm")]
public class LLMController : Controller
{
    private readonly LLMService _llmService;

    public LLMController(LLMService llmService)
    {
        _llmService = llmService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateResponse([FromBody] LLMRequest request)
    {
        var response = await _llmService.HandleUserRequest(request.Text);
        return Ok(new { response });
    }
}

public class LLMRequest
{
    public required string Text { get; set; }
}

}
