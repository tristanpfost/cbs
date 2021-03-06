using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using Read.StaffUsers.Models;

namespace Read.StaffUsers
{
    public class StaffUsers : IStaffUsers
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BaseUser> _collection;

        public StaffUsers(IMongoDatabase database)
        {
            _database = database;
            _collection = database.GetCollection<BaseUser>("StaffUsers");
        }


        public T GetById<T>(Guid id) where T : BaseUser
        {
            var user = _collection.FindSync(u => u.StaffUserId == id).FirstOrDefault();
            if (user == null)
            {
                throw new UserNotFound($"StaffUser with id {id} was not found");
            }
            var result = user as T;

            if (result == null)
            {
                // User is not of request type
                throw new UserNotOfExpectedType($"User with id {id} was is not of type {nameof(T)}");
            }
            
            return result;
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : BaseUser
        {
            var user = (await _collection.FindAsync(u => u.StaffUserId == id)).FirstOrDefault();

            if (user == null)
            {
                throw new UserNotFound($"StaffUser with id {id} was not found");
            }
            var result = user as T;

            if (result == null)
            {
                throw new UserNotOfExpectedType($"User with id {id} was is not of type {nameof(T)}");
            }

            return result;
        }

        public IEnumerable<T> GetAll<T>() where T : BaseUser
        {
            return _collection.AsQueryable().OfType<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseUser
        {
            var filter = Builders<BaseUser>.Filter.OfType<T>();
            var cursor = await _collection.FindAsync(filter);
            var res = await cursor.ToListAsync();
            // This should be safe..
            return res.Cast<T>().ToList();

        }

        public void Remove(Guid id)
        {
            var res = _collection.DeleteOne(u => u.StaffUserId == id);
            if (res.DeletedCount == 0)
            {
                throw new UserNotFound($"StaffUser with id {id} was not found");
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var res =  await _collection.DeleteOneAsync(u => u.StaffUserId == id);
            if (res.DeletedCount == 0)
            {
                throw new UserNotFound($"StaffUser with id {id} was not found");
            }
        }

        public void Save<T>(T dataCollector) where T : BaseUser
        {
            _collection.ReplaceOne(u => u.StaffUserId == dataCollector.StaffUserId, dataCollector, new UpdateOptions { IsUpsert = true });
        }

        public async Task SaveAsync<T>(T dataCollector) where T : BaseUser
        {
            await _collection.ReplaceOneAsync(u => u.StaffUserId == dataCollector.StaffUserId, dataCollector, new UpdateOptions { IsUpsert = true });
        }
    }
}
