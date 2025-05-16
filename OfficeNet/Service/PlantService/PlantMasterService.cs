using Microsoft.EntityFrameworkCore;
using OfficeNet.Domain.Entities;
using OfficeNet.Infrastructure.Context;

namespace OfficeNet.Service.PlantService
{
    public class PlantMasterService : IPlantsMasterService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlantMasterService> _logger;

        public PlantMasterService(ApplicationDbContext context, ILogger<PlantMasterService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Task DeletePlantAsync(int plantId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlantByNameAsync(string plantName)
        {
            throw new NotImplementedException();
        }

        public Task<Plant> GetPlantByIdAsync(Plant plantId)
        {
            throw new NotImplementedException();
        }

        public Task<Plant> GetPlantByNameAsync(Plant plantName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Plant>> GetPlantListAsync()
        {
            try
            {
                var result = await _context.Plants.ToListAsync();
                if (result == null || result.Count == 0)
                    throw new ArgumentNullException("No plants  are available");

                _logger.LogInformation("Data Get Successfully");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while fetching data", ex);
                throw new Exception("There is error while fetching data", ex);
            }
        }

        public async Task<Plant> SavePlantAsync(Plant objPlant)
        {
            if (objPlant == null)
                throw new ArgumentNullException(nameof(objPlant));
            try
            {
                _context.Plants.Add(objPlant);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Plant Saved successfully");
                return objPlant;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while saving the plant.", ex);
                throw new Exception("An error occurred while saving the plant.", ex);
            }
        }


        public Task<Plant> UpdatePlantAsync(Plant objPlant)
        {
            throw new NotImplementedException();
        }
    }
}
