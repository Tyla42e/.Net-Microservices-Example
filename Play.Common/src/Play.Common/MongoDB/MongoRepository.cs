using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common;

namespace Play.Common.MongoDb
{
    public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T : IEntity
    {


        private readonly IMongoCollection<T> _dbCollection = database.GetCollection<T>(collectionName);

        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public async Task<IReadOnlyCollection<T>> getAllAsync()
        {
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> getAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbCollection.InsertOneAsync(entity);

        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<T> filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);

            await _dbCollection.ReplaceOneAsync(filter, entity);

        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }


    }
}