using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gamewebapi
{
    [Route("api/players/")]
    [ApiController]
    public class PlayerController :Controller{

        private IRepository _repository;

        public PlayerController(IRepository repository) => _repository = repository;


        [HttpGet]
        [Route("{playerId:int}")]
        public async Task<Player> Get(Guid id){
            return await _repository.Get(id);
        }
         
        
        // [HttpGet]
        // public async Task<IActionResult> GetPlayerWithName(string name){
        //     name = HttpContext.Request.Query["name"][0];
        //     return Ok(await _repository.GetPlayerWithName(name));
        // }
        
        [HttpGet]
        [Route("itemtypequery/{itemtype}")]
        public async Task<Player[]> GetPlayersWithItemType(ItemType itemType){
            return await _repository.GetPlayersWithItemType(itemType);
        }
        [HttpGet]
        [Route("scorequery/{score}")]
        public async Task<Player[]> GetPlayersWithScore(int score){
            return await _repository.GetPlayersWithScore(score);
        }

        [HttpPost]
        [Route("{playerId}&{score}")]
        public async Task<Player> IncrementPlayerScore(Guid id, int increment){
            return await _repository.IncrementPlayerScore(id,increment);
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
                Name = player.Name,
                CreationTime = DateTime.Now
            };
            //await _repository.Create(newplayer);
            return await _repository.Create(newplayer);
        }
        [HttpPost]
        [Route("{playerId}")]
        public Task<Player> Modify(Guid id, Player modifiedPlayer){
            return _repository.Modify(id, modifiedPlayer);
        }
        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Delete(Guid id){
            return _repository.Delete(id);
        }
        
            
    }
}
