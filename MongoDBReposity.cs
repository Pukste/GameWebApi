using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

 
namespace gamewebapi
{
    public class MongoDbRepository : IRepository
    {
        private IMongoCollection<Player> _collection;
 
        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("name");
            _collection = database.GetCollection<Player>("players");
        }
 
        public async Task<Player> Create(Player player)
        {
            player.Id = Guid.NewGuid();
            player.CreationTime = DateTime.Now;
            await _collection.InsertOneAsync(player);
            return player;
        }
 
        public async Task<Item> CreateItem(Guid id, Item newItem)
        {
            newItem.Id = Guid.NewGuid();
 
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var update = Builders<Player>.Update.Push(e => e.Items, newItem);
            await _collection.FindOneAndUpdateAsync(filter,update);
 
            return newItem;
        }
 
        public async Task<Player> Delete(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            await _collection.DeleteOneAsync(filter);
 
            return player;
        }
 
        // not working yet..
        public async Task<Item> DeleteItem(Guid id, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
            Item olditem = new Item();
            for (int i = 0; i < player.Items.Count; i++)
            {
                if (player.Items[i].Id == itemId)
                {
                    olditem = player.Items[i];
                }
            }
            var update = Builders<Player>.Update.PullFilter(p =>p.Items, f=>f.Id == itemId);
            await _collection.FindOneAndUpdateAsync(p => p.Id == id,update);
            return olditem;
        }
 
        public async Task<Player> Get(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
            return player;
        }
 
        public async Task<Item> GetItem(Guid id, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            for (int i = 0; i < player.Items.Count; i++)
            {
                if (player.Items[i].Id == itemId)
                {
                    return player.Items[i];
                }
            }
 
            return null;
        }
 
        public Task<Player[]> GetAll()
        {
            var list = _collection.Find(p => true).ToList();
            Player[] myArray = list.ToArray();
            return Task.FromResult(myArray);
        }
 
        public async Task<Item[]> GetAllItems(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            return player.Items.ToArray();
        }
 
        //replacing the current player completely atm
        public async Task<Player> Modify(Guid id, Player player)
        {
            player.Id=id;
            await _collection.ReplaceOneAsync(p => p.Id == id ,player);
            return player;
        }

        // replacing the current item completely atm also not working.       
        public async Task<Item> UpdateItem(Guid id, Guid itemId, Item updateItem) 
        {
 
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            for (int i = 0; i < player.Items.Count; i++)
            {
                if (player.Items[i].Id == itemId)
                {
                    Item oldItem = player.Items[i];
                    player.Items[i] = updateItem;
                    var update = Builders<Player>.Update.PullFilter(p =>p.Items, f=>f.Id == itemId);
                    await _collection.FindOneAndUpdateAsync(filter,update);
                    return oldItem;
                }
            }
 
            return null;
        }
    }
}

