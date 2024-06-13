using HP.API.Models.Domain;
using HP.API.Models.DTOs;

namespace HP.API.Repositories
{
    public interface IConsultationRepository
    {
        Task<Consultation> CreateAsync(AddConsultationRequestDto addConsultationRequestDto);
        Task<List<Consultation>> GetAllAsync();
        Task<Consultation> UpdateStatusAsync(UpdateTextConsStatusRequestDto model);
        Task<bool> CheckValidConsAsync(string userId,DateTime date, int typeId);
        Task<List<Consultation>> GetTodaysAsync(DateTime date,string? userId);
        Task<List<Consultation>> GetAllByUserAsync(string id);
        Task<Consultation> GetByIdAsync(Guid id);
        Task<List<Consultation>> GetByVetAsync(string id,DateTime date);
        Task<List<Slot>> GetAllSlotsAsync(DateTime date);
        Task<List<Slot>> GetAvailableSlotsAsync(DateTime date);
        Task<List<Consultation>> GetVideoAsync(string? id,DateTime? date,bool? done);
        Task<List<Consultation>> GetEmergencyAsync(string? id,DateTime? date,bool? done);
        Task<List<Consultation>> GetTextAsync(string? id,DateTime? date,bool? today,bool? done,Guid? consId);
        Task<Consultation> StartConsultationAsync(Guid consId, string userId);
    }
}
