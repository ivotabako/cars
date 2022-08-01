using Cars.Api.Entity;
using Cars.Api.Settings;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cars.Api.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly IMongoCollection<CarEntity> _carsCollection;

        public CarsRepository(
            IOptions<CarsSettings> settings, IConfiguration config)
        {
            var mongoClient = new MongoClient(
                config["MongoConnectionString"]);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _carsCollection = mongoDatabase.GetCollection<CarEntity>(
                settings.Value.CarsCollectionName);
        }

        public async Task<List<CarEntity>> GetAsync() =>
            await _carsCollection.Find(_ => true).ToListAsync();

        public async Task<CarEntity?> GetAsync(string id) =>
            await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CarEntity newCar)
        {
            await _carsCollection.InsertOneAsync(newCar);
        }

        public async Task UpdateAsync(string id, CarEntity updatedCar)
        {
            await _carsCollection.ReplaceOneAsync(x => x.Id == id, updatedCar);
        }

        public async Task RemoveAsync(string id) =>
            await _carsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
