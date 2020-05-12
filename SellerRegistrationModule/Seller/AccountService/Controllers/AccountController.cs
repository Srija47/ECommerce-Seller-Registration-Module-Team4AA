using AccountService.Entities;
using AccountService.Manager;
using AccountService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _iAccountManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController( IAccountManager iAccountManager,ILogger<AccountController> logger)
        {
            _iAccountManager = iAccountManager;
            _logger = logger;
        }

        /// <summary>
        /// Creates a New Seller.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /SellerRegister
        ///     {
        ///        "id": 1,
        ///        "name": "seller1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="seller"></param>
        /// <returns>A newly created SellerRegister</returns>
        /// <response code="201">Returns the newly created seller</response>
        /// <response code="400">If the seller is null</response>            
        [HttpPost]
        [Route("RegisterSeller")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SellerRegister(SellerRegister seller)
        {
            _logger.LogInformation("Register");
            if (seller is null)
            {
                return BadRequest("Seller is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //Null Checking -
            await _iAccountManager.SellerRegister(seller);
            _logger.LogInformation($"Succesfully Registered");
            return Ok();
        }
        /// <summary>
        /// Login as a specific User.
        /// </summary>
        /// <param name="uname"></param> 
        /// <param name="pwd"></param>
        [HttpGet]
        [Route("SellerLogin/{uname}/{pwd}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SellerLogin(string uname, string pwd)
        {
               _logger.LogInformation("Login");
                var seller=await _iAccountManager.ValidateSeller(uname, pwd);

                if (seller != null)
                {
                    //Null Checking-
                    return Ok(seller);
                }
                _logger.LogInformation($"Welcome {seller.Username}");
                return Ok(seller);
        }
    }
}