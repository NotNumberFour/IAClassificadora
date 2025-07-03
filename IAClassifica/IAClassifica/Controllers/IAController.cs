using Engine;
using Microsoft.AspNetCore.Mvc;

namespace IAClassifica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IAController : ControllerBase
    {
        private readonly EngineV1 engineV1;
        private readonly ILogger<IAController> _logger;

        public IAController(ILogger<IAController> logger, EngineV1 engineV1)
        {
            _logger = logger;
            this.engineV1 = engineV1;
        }

        [HttpGet]
        public void Get()
        {
            this.engineV1.Treinar();
        }
        [HttpPost]
        public int post([FromBody] float[] imagem)
        {
           return this.engineV1.Prever(imagem);
        }
    }
}
