using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gamewebapi
{
    [Route("api/players/")]
    [ApiController]
    public class PlayerController{

        private IRepository _repository;

        public PlayerController(IRepository repository) => _repository = repository;


        [HttpGet]
        [Route("{playerId}")]
        public async Task<Player> Get(Guid id){
            return await _repository.Get(id);
        }
        [HttpGet]
        [Route("")]
        public Task<Player[]> GetAll(){
            return _repository.GetAll();
        }
        [HttpPost]
        [Route("")]
        public async Task<Player> Create(NewPlayer player){
            //_logger.LogInformation("Creating player with name " + newPlayer.Name);
            var newplayer = new Player()
            {
                Id = Guid.NewGuid(),
                Name = player.Name
            };
            //await _repository.Create(newplayer);
            return await _repository.Create(newplayer);
        }
        [HttpPost]
        [Route("{playerId}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer){
            return _repository.Modify(id, modifiedPlayer);
        }
        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Delete(Guid id){
            return _repository.Delete(id);
        }
        [HttpPost]
        [Route("{playerId}/items/")]
        public Task<Item> CreateItem(Guid playerId, Item item){
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("{playerId}/items/{itemId}")]
        public Task<Item> GetItem(Guid playerId, Guid itemId){
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("{playerId}/items/")]
        public Task<Item[]> GetAllItems(Guid playerId){
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("{playerId}/items/{itemId}")]
        public Task<Item> UpdateItem(Guid playerId, Item item){
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("{playerId}/items/{itemId}")]
        public Task<Item> DeleteItem(Guid playerId, Item item){
            throw new NotImplementedException();
        }
            
            
    }
}
