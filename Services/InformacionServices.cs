using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PanchitoProyectApi.Configuration;
using PanchitoProyectApi.Models;

namespace PanchitoProyectApi.Services
{
    public class InformacionServices
    {
        private readonly IMongoCollection<Informacion> _infoCollection;

        public InformacionServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _infoCollection = mongoDB.GetCollection<Informacion>(databaseSettings.Value.CollectionName);
        }




        public async Task<List<Informacion>> GetAsync() => await _infoCollection.Find(_ => true).ToListAsync();

        public async Task<Informacion> GetInformacionById(string Id)
        {
            return await _infoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
        }

        public async Task InsertInformacion(Informacion informacion)
        {
            await _infoCollection.InsertOneAsync(informacion);
        }

        public async Task UpdateInformacion(Informacion informacion)
        {
            var filter = Builders<Informacion>.Filter.Eq(s => s.Id, informacion.Id);
            await _infoCollection.ReplaceOneAsync(filter, informacion);
        }

    }
}
