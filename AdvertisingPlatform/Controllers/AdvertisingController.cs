using AdvertisingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvertisingPlatform.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdvertisingController : ControllerBase
    {

        private readonly AdvertisingPlatformService advertisingPlatformService;

        public AdvertisingController()
        {
            this.advertisingPlatformService = new AdvertisingPlatformService();
        }

        //LocationNode Locations;

        // POST api/v1/<AdvertisingController>
        [HttpPost("RefreshDictionary")]
        public async Task<IActionResult> Post()
        {
            var rawRequestBody = await Request.GetRawBodyAsync();

            // Other code here

            return Ok();
        }

        // GET: api/v1/<AdvertisingController>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery(Name = "location")] string location)
        {
            if (ExtensionMethods.LocalsRootNode == null)
            {
                return ["Загрузите файл"];
            }

            List<string> list = [];
            ExtensionMethods.LocalsRootNode.SeekByLocation(location, list, 0);
            return [.. list];            
        }

    }
}
