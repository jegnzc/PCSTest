﻿using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.MongoDB;

public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbContext context)
    {
        var collectionName = typeof(T).Name.ToLowerInvariant();
        _collection = context.GetCollection<T>(collectionName);
    }

    public async Task CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }


    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await _collection.DeleteOneAsync(filter);
    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(c => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(string id, T entity)
    {
        entity.Id = id;

        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
    }

}
