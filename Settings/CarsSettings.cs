namespace Cars.Api.Settings
{
    public class CarsSettings
    {
        public string MongoConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CarsCollectionName { get; set; } = null!;
    }
}
