using Corvus.LineBot.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corvus.LineBot.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly LineBotService _linebot;
        private readonly GptService _gpt;

        public LineBotController(LineBotService linebot, GptService gpt)
        {
            _linebot = linebot;
            _gpt = gpt;
        }

        [HttpPost]
        public async Task<IActionResult> WebHook()
        {
            await _linebot.WebHook(Request);

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GptPost()
        {
            //var result = await _gpt.PostGPT("你好！");
            var result = await _gpt.PostGPT("如何使用chatGpt api");

            return Ok(result);
        }
    }
}
