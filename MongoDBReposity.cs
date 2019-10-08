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
            var database = mongoClient.GetDatabase("Game");
            _collection = database.GetCollection<Player>("players");
        }
 
        public async Task<Player> Create(Player player)
        {
            player.Id = Guid.NewGuid();
 
            await _collection.InsertOneAsync(player);
            return player;
        }
 
        public async Task<Item> CreateItem(Guid id, Item newItem)
        {
            newItem.Id = Guid.NewGuid();
 
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            player.Items.Add(newItem);
 
            return player.Items.Last();
        }
 
        public async Task<Player> Delete(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            await _collection.DeleteOneAsync(filter);
 
            return player;
        }
 
        public async Task<Item> DeleteItem(Guid id, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var playerCursor = await _collection.FindAsync(filter);
            Player player = await playerCursor.FirstAsync();
 
            for (int i = 0; i < player.Items.Count; i++)
            {
                if (player.Items[i].Id == itemId)
                {
                    Item deletedItem = player.Items[i];
                    player.Items.RemoveAt(i);
                    return deletedItem;
                }
            }
 
            return null;
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
 
        
        public async Task<Player> Modify(Guid id, Player player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            await _collection.ReplaceOneAsync(filter,player);
            return player;
        }
 
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
                    return oldItem;
                }
            }
 
            return null;
        }
    }
}

