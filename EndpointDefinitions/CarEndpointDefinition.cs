using AutoMapper;
using Cars.Api.Entity;
using Cars.Api.Infrastructure;
using Cars.Api.Models;
using Cars.Api.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Bson;

namespace Cars.Api.EndpointDefinitions
{
    public class CarEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/api/cars", async (ICarsRepository db) =>
            {
                return await db.GetAsync();
            });
            app.MapGet("/api/cars/{id}", async (string id, ICarsRepository db) =>
            {
                var car = await db.GetAsync(id);
                return Results.Ok(car);
            });
            app.MapPost("/api/cars", async (CarCreateModel model, ICarsRepository db, IMapper mapper, IValidator<CarCreateModel> validator) =>
            {
                ValidationResult result = await validator.ValidateAsync(model);

                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }

                var id = ObjectId.GenerateNewId();
                var newCar = mapper.Map<CarEntity>(model);
                newCar.Id = id.ToString();

                await db.CreateAsync(newCar);

                return Results.Created($"/api/cars/{id}", newCar);
            });
            app.MapPut("/api/cars/{id}", async (string id, CarModel model, IMapper mapper, ICarsRepository db, IValidator<CarModel> validator) =>
            {
                var carOld = await db.GetAsync(id);
                if (carOld == null)
                {
                    return Results.NotFound();
                }
                if (!string.Equals(model.Id, id))
                {
                    return Results.BadRequest();
                }
                ValidationResult result = await validator.ValidateAsync(model);

                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }
                var newCar = mapper.Map<CarEntity>(model);
                await db.UpdateAsync(id, newCar);

                return Results.NoContent();
            });
            app.MapDelete("/api/cars/{id}", async (string id, ICarsRepository db) =>
            {
                var carOld = await db.GetAsync(id);
                if (carOld == null)
                {
                    return Results.NotFound();
                }
                await db.RemoveAsync(id);

                return Results.NoContent();
            });
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<ICarsRepository, CarsRepository>();
        }
    }
}
