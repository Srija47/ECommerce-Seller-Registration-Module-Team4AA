using System;
using System.Threading.Tasks;
using ItemService.Manager;
using ItemService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemManager _manager;
        private readonly ILogger<ItemController> _logger;
        public ItemController(IItemManager manager, ILogger<ItemController> logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpPost]
        [Route("AddItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(ItemsModel item)
        {
            _logger.LogInformation("AddItem");
            if (item is null)
            {
                return BadRequest("Item is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _manager.AddItemsManager(item);
            return Ok();

        }
        [HttpDelete]
        [Route("DeleteItem/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(ItemsModel itemid)
        {
            _manager.DeleteItemsManager(itemid);
            return Ok();
            throw new Exception("Exception while deleting the item from the storage.");


        }
        [HttpGet]
        [Route("ViewItems/{sellerid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ViewItems(int sellerid)
        {
            return Ok(_manager.ViewItemsManager(sellerid));
            throw new Exception("Exception while retrieving the items from the storage.");

        }
        [HttpPut]
        [Route("ViewItems/{sellerid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateItems(ItemsModel itemid)
        {
            return Ok(_manager.UpdateItemsManager(itemid));
            throw new Exception("Exception while retrieving the items from the storage.");

        }
    }
}