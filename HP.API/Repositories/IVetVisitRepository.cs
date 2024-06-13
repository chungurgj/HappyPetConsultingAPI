using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Repositories
{
    public interface IVetVisitRepository
    {
        Task<VetVisit> CreateAsync(AddVetVisitRequestDto addVetVisitRequestDto);
        Task<List<VetVisit>> GetAllAsync(FilterVetVisitDto filterVetVisitDto);
        Task<List<Slot>> GetAvailableSlotsAsync(DateTime date);
        Task<List<Slot>> GetAllSlotsAsync(DateTime date);
        Task<List<VetVisit>> GetByUserAsync(string userId);
        Task<VetVisit> UpdateAsync(UpdateVetVisitRequestDto updateVetVisitRequestDto);
        Task<VetVisit> DeleteAsync(Guid id,string message);
        Task<Slot> GetTimeByIdAsync(Guid id);
    }
}
