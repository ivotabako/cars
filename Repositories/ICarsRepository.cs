using Cars.Api.Entity;
using FluentValidation.Results;

namespace Cars.Api.Repositories
{
    public interface ICarsRepository
    {
        Task CreateAsync(CarEntity newCar);
        Task<List<CarEntity>> GetAsync();
        Task<CarEntity?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, CarEntity updatedCar);
        //Task<ValidationResult> ValidateAsync(CarEntity newCar);
    }
}