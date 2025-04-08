using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/llm")]
public class LLMController : ControllerBase
{
    private readonly LLMService _llmService;

    public LLMController(LLMService llmService)
    {
        _llmService = llmService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateResponse([FromBody] LLMRequest request)
    {
        var response = await _llmService.GetModelResponse(request.Text);
        return Ok(new { response });
    }
}

public class LLMRequest
{
    public string Text { get; set; }
}
