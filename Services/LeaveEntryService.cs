using RestApiWithMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RestApiWithMongoDb.Services;

public class LeaveEntryService
{
    private readonly IMongoCollection<LeaveEntry> _leaveEntryCollection;

    public LeaveEntryService(IMongoDatabase mongoDatabase)
    {
        _leaveEntryCollection = mongoDatabase.GetCollection<LeaveEntry>("LeaveEntry");
    }

    public async Task<List<LeaveEntry>> GetAsync() =>
        await _leaveEntryCollection.Find(_ => true).ToListAsync();

    public async Task<LeaveEntry?> GetAsync(string id) =>
        await _leaveEntryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(LeaveEntry newLeaveEntry) =>
        await _leaveEntryCollection.InsertOneAsync(newLeaveEntry);

    public async Task UpdateAsync(string id, LeaveEntry updatedLeaveEntry) =>
        await _leaveEntryCollection.ReplaceOneAsync(x => x.Id == id, updatedLeaveEntry);

    public async Task RemoveAsync(string id) =>
        await _leaveEntryCollection.DeleteOneAsync(x => x.Id == id);
}