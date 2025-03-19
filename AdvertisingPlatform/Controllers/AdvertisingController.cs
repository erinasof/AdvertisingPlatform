using AdvertisingPlatform.Services;
using AdvertisingPlatform.Model;
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

        // POST api/v1/<AdvertisingController>
        [HttpPost("RefreshDictionary")]
        public async Task<IActionResult> RefreshDictionary()
        {
            try
            {
                string referenceFileContent = await Request.GetRawBodyAsync();
                if (referenceFileContent == null || string.Empty == referenceFileContent)
                {
                    return UnprocessableEntity(new ErrorMessage { Error = "Empty file" });
                }
                advertisingPlatformService.RefreshLocationReference(referenceFileContent);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorMessage { Error = ex.Message });
            }
        }

        // GET: api/v1/<AdvertisingController>
        [HttpGet]
        public IActionResult SeekByLocation([FromQuery(Name = "location")] string location)
        {
            if (advertisingPlatformService.IsReferenceEmpty())
            {
                return BadRequest(new ErrorMessage
                {
                    Error = "Location's reference data is empty, please upload file first" 
                });
            }
            try
            {
                return Ok(new PlatformResponce { AdvPlatforms = advertisingPlatformService.SeekByLocation(location) });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage { Error = ex.Message });
            }
            
        }

    }
}
