using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> HelloWorld()
        {
            return Ok("Hello World!");
        }
    }
}
