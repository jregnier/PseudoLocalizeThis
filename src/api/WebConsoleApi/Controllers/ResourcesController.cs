using Microsoft.AspNetCore.Mvc;
using TransformLib.Services;
using TransformLib.Transforms;

namespace WebConsoleApi.Controllers
{
    [Route("api/[controller]")]
    public class ResourcesController : Controller
    {
        [HttpGet]
        public IActionResult Get(string filePath)
        {
            var service = new ResourceFileService();
            service.Read(filePath);

            return Ok(service.GetTransformedElements(
                new TransformSettings() { Brackets = true, Larger = true, Mirror = true }));
        }
    }
}