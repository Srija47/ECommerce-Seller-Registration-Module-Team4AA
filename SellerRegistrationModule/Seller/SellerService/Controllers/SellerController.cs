using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SellerService.Manager;
using SellerService.Models;
using System.Threading.Tasks;

namespace SellerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerManager _iSellerManager;
        private readonly ILogger<SellerController> _logger;

        public SellerController(ISellerManager iSellerManager, ILogger<SellerController> logger)
        {
            _iSellerManager = iSellerManager;
            _logger = logger;
        }
        [HttpPut]
        [Route("EditProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSellerProfile(SellerProfile seller)
        {
            _logger.LogInformation("EditPROFILE");
            return Ok(await _iSellerManager.UpdateSellerProfile(seller));

        }
        [HttpGet]
        [Route("GetProfile/{sellerid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfileAsync(int sellerid)
        {
            _logger.LogInformation("ViewProfile");
            return Ok(await _iSellerManager.ViewSellerProfile(sellerid));

        }

    }
}