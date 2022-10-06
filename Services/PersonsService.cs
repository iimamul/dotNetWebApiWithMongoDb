using RestApiWithMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RestApiWithMongoDb.Services;

public class PersonsService
{
    private readonly IMongoCollection<Person> _PersonsCollection;

    public PersonsService(IMongoDatabase mongoDatabase)
    {
        _PersonsCollection = mongoDatabase.GetCollection<Person>("Persons");
    }

    public async Task<List<Person>> GetAsync() =>
        await _PersonsCollection.Find(_ => true).ToListAsync();

    public async Task<Person?> GetAsync(string id) =>
        await _PersonsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Person newPerson) =>
        await _PersonsCollection.InsertOneAsync(newPerson);

    public async Task UpdateAsync(string id, Person updatedPerson) =>
        await _PersonsCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);

    public async Task RemoveAsync(string id) =>
        await _PersonsCollection.DeleteOneAsync(x => x.Id == id);
}