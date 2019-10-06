using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace gamewebapi{
    public class FileRepository: IRepository{
        
        
        public Task<Player> Get(Guid id){
            string[] _repository = File.ReadAllLines("game_dev.txt", Encoding.Default);
            foreach(var player in _repository){
                Console.WriteLine(player);
                var p =JsonConvert.DeserializeObject<Player>(player);
                if(p.Id == id){
                    return Task.FromResult(p);
                }
            }
            return null;
        }
        public Task<Player[]> GetAll(){
            string[] _repository = File.ReadAllLines("game_dev.txt", Encoding.Default);
            List<Player> plist = new List<Player>();
            foreach(var player in _repository){
                plist.Add(JsonConvert.DeserializeObject<Player>(player));
            }
            
            return Task.FromResult(plist.ToArray());
        }
        public Task<Player> Create(Player player){
            string[] _repository = File.ReadAllLines("game_dev.txt", Encoding.Default);
           // PlayerList players = JsonConvert.DeserializeObject<PlayerList>(_repository);
            var newplayer = new Player()
            {
                Id = Guid.NewGuid(),
                Name = player.Name
            };
            List<string> newlist = _repository.ToList();
            newlist.Add(JsonConvert.SerializeObject(newplayer));
           // players.allPlayers.Add(newplayer); // heittää nullreferencen atm
            //File.AppendAllText("game_dev.txt", JsonConvert.SerializeObject(newplayer));
            File.WriteAllLines("game_dev.txt", newlist.ToArray());
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