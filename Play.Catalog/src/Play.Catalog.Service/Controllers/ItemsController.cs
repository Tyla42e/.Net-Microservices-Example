using Microsoft.AspNetCore.Mvc;

using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Models;
using Play.Common;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly  IRepository<Item> _itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            this._itemsRepository = itemsRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync() 
        {
            var items = (await _itemsRepository.getAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);

            if (item == null)  {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto) 
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description =createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
                };


            await _itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto uiDto) {
            var existingItem = await _itemsRepository.GetAsync(id);

            if (existingItem == null) {
                return NotFound();
            }

            existingItem.Name = uiDto.Name;
            existingItem.Description = uiDto.Description;
            existingItem.Price = uiDto.Price;

            await _itemsRepository.UpdateAsync(existingItem);
           

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);

            if (item == null) {
                return NotFound();
            }

        
            await _itemsRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}