using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Linq;

namespace gamewebapi{
    public class FileRepository: IRepository{
        
        
        public Task<Player> Get(Guid id){
            string _repository = File.ReadAllText("game_dev.txt", Encoding.Default);
            PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            foreach(var player in players.allPlayers){
                if(player.Id == id){
                    return Task.FromResult(player);
                }
            }
            return null;
        }
        public Task<Player[]> GetAll(){
            string _repository = File.ReadAllText("game_dev.txt", Encoding.Default);
            PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            
            
            return Task.FromResult(players.allPlayers.ToArray());
        }
        public Task<Player> Create(Player player){
            string[] _repository = File.ReadAllLines("game_dev.txt", Encoding.Default);
           // PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            var newplayer = new Player()
            {
                Id = Guid.NewGuid(),
                Name = player.Name
            };
           // players.allPlayers.Add(newplayer); // heittää nullreferencen atm
           File.AppendAllText("game_dev.txt", JsonConvert.SerializeObject(newplayer));
            //File.WriteAllText("game_dev.txt", JsonConvert.SerializeObject(players));
            return Task.FromResult(newplayer);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){

            string _repository = File.ReadAllText("game_dev.txt", Encoding.Default);
            PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            foreach(var oldplayer in players.allPlayers){
                if(oldplayer.Id == id){
                    oldplayer.Score = player.Score;
                    File.WriteAllText("game_dev.txt", JsonConvert.SerializeObject(players));
                    return Task.FromResult(oldplayer);
                }
            }
            return null;
        }
        public Task<Player> Delete(Guid id){

            string _repository = File.ReadAllText("game_dev.txt", Encoding.Default);
            PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            for(int i = players.allPlayers.Count()-1; i >= 0; i--){
                if(players.allPlayers[i].Id == id){
                    Player deletedPlayer = players.allPlayers[i];
                    players.allPlayers.RemoveAt(i);
                    File.WriteAllText("game_dev.txt", JsonConvert.SerializeObject(players));
                    return Task.FromResult(deletedPlayer);
                }
            }
            return null;
        }
    }
}