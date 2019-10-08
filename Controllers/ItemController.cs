using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gamewebapi
{
    [Route("api/players/{playerId}/items/")]
    [ApiController]
    public class ItemController{

        private IRepository _itemrepository;

        public ItemController(IRepository repository) => _itemrepository = repository;

        [HttpPost]
        [Route("")]
        public Task<Item> CreateItem(Guid playerId, NewItem item){
            var newit = new Item(){
                Id = Guid.NewGuid(),
                ItemType = item.ItemType,
                Price = item.Price
            };
            return CreateItem(playerId, item);
        }
        [HttpGet]
        [Route("{itemId}")]
        public Task<Item> GetItem(Guid playerId, Guid itemId){
            return GetItem(playerId,itemId);
        }
        [HttpGet]
        [Route("")]
        public Task<Item[]> GetAllItems(Guid playerId){
            return GetAllItems(playerId);
        }
        [HttpPost]
        [Route("{itemId}")]
        public Task<Item> UpdateItem(Guid playerId, Guid itemId, UpdateItem item){
            return UpdateItem(playerId, itemId, item);
        }
        [HttpDelete]
        [Route("{itemId}")]
        public Task<Item> DeleteItem(Guid playerId, Item item){
            return DeleteItem(playerId, item);
        }


    }

}