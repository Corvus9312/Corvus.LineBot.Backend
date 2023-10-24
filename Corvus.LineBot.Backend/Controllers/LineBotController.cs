using Corvus.LineBot.Backend.Models;
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

        [HttpPost]
        public IActionResult PushMessage(PostMessageReqVM req)
        {
            var result = _linebot.PushMessage(req);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GptPost(string msg)
        {
            var result = await _gpt.PostGPT("Corvus", msg);

            return Ok(result);
        }
    }
}
