using HP.API.Data;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HP.API.Repositories
{
    public class SQLConsultationRepository : IConsultationRepository
    {
        private readonly HPDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly ILogger<Slot> logger;

        public SQLConsultationRepository(HPDbContext dbContext,UserManager<User> userManager, ILogger<Slot> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<bool> CheckValidConsAsync(string userId, DateTime date, int typeId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var type = await dbContext.ConsultationTypes.FindAsync(typeId);
            if(type == null)
            {
                throw new Exception("This type of consultation is not found");
            }

            var userConsultations = await dbContext.Consultations
                .FirstOrDefaultAsync(p => p.Owner_Id == user.Id && p.Created.Date == date.Date && p.Done==false && p.Type_Id==typeId);

            if(userConsultations!=null)
            {
                return false;
            }

            return true;
        }


        public async Task<Consultation> CreateAsync(AddConsultationRequestDto addConsultationRequestDto)
        {
            var vet = await userManager.FindByIdAsync(addConsultationRequestDto.Vet_Id);

            if (vet == null)
            {
                throw new Exception("Vet not found");
            }

            var pet = await dbContext.Pets.FindAsync(addConsultationRequestDto.Pet_Id);
            if (pet == null)
            {
                throw new Exception("Pet not found");
            }

            var owner = await userManager.FindByIdAsync(pet.Owner_Id);
            if (owner == null)
            {
                throw new Exception("Owner not found");
            }
            var categoryType = await dbContext.ConsultationTypes.FindAsync(addConsultationRequestDto.Type_Id);


            if (categoryType == null)
            {
                throw new Exception("Category not found");
            }

            DateTime? consStart = null;
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);

            if (categoryType.Id == 1)
            {
                consStart = localTime;
            }
            else if (categoryType.Id == 2 || categoryType.Id == 3)
            {
                if (addConsultationRequestDto.ConsultationStart == null)
                {
                    throw new Exception("The consultation start time is not defined");
                }

                consStart = addConsultationRequestDto.ConsultationStart.Value.Date
                   .AddHours(addConsultationRequestDto.ConsultationStart.Value.Hour)
                   .AddMinutes(addConsultationRequestDto.ConsultationStart.Value.Minute);
            
        }
           

            var consultation = new Consultation
            {

                Pet_Id = addConsultationRequestDto.Pet_Id,
                Owner_Id = owner.Id,
                Vet_Id = addConsultationRequestDto.Vet_Id,
                Pet_Name = pet.Name,
                Pet_Age = pet.Age,
                Pet_Breed = pet.Breed,
                Owner_Name = owner.DisplayName,
                Owner_Email = owner.Email,
                Vet_Name = vet.DisplayName,
                ConsultationStart = consStart,
                Created = localTime,
                Done = false,
                Des = addConsultationRequestDto.Des,
                Price = addConsultationRequestDto.Price ?? categoryType.Price,
                ConsultationType = categoryType,
                Type_Id = categoryType.Id
            };

            await dbContext.Consultations.AddAsync(consultation);
            await dbContext.SaveChangesAsync();

            return consultation;
        }


        public async Task<List<Consultation>> GetAllAsync()
        {
            return await dbContext.Consultations.Include(p=>p.ConsultationType).ToListAsync();

        }

        public async Task<List<Consultation>> GetAllByUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new Exception("The user has not been found");
            }

            var textCons = await dbContext.Consultations.Where(p=>p.Owner_Id == user.Id).ToListAsync();

            return textCons;
        }

        public async Task<List<Slot>> GetAllSlotsAsync(DateTime date)
        {
            var slots = new List<Slot>();

            var startWorkTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            var endWorkTime = startWorkTime.AddHours(12);

            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            var consultations = await dbContext.Consultations
                .Where(c => c.ConsultationType.Id == 2 &&
                        c.ConsultationStart != null &&
                        c.ConsultationStart >= startWorkTime &&
                        c.ConsultationStart <= endWorkTime)
                .ToListAsync();

            var bookedSlots = new HashSet<DateTime>();

            foreach (var consultation in consultations)
            {
                var consStartTime = consultation.ConsultationStart;
                var consEndTime = consStartTime.Value.AddMinutes(30);

                while (consStartTime < consEndTime)
                {
                    bookedSlots.Add(consStartTime.Value);
                    consStartTime = consStartTime.Value.AddMinutes(30);
                }
            }

            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);
            localTime = localTime.AddHours(2);

            while (startWorkTime < endWorkTime)
            {
                var done = bookedSlots.Contains(startWorkTime);
                var isPassed = startWorkTime < localTime;
                slots.Add(new Slot
                {
                    Time = startWorkTime,
                    isAvailable = !done,
                    SlotType = SlotType.Consultation,
                    Possible = !isPassed
                });
                startWorkTime = startWorkTime.AddMinutes(30);
            }

            return slots;
        }


        public Task<List<Slot>> GetAvailableSlotsAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<Consultation> GetByIdAsync(Guid id)
        {
            var textCons = await dbContext.Consultations.FindAsync(id);

            if (textCons == null)
            {
                throw new Exception("Text consultation not found");
            }
            return textCons;
        }

        public async Task<List<Consultation>> GetByVetAsync(string id, DateTime date)
        {
            
            var vet = await userManager.FindByIdAsync(id);

            if (vet == null)
            {
                throw new Exception("The user/vet not found");
            }

            var textCons = await dbContext.Consultations.Where(c=>c.Vet_Id== vet.Id).ToListAsync();
            


            IQueryable<Consultation> query = dbContext.Consultations.Where(c => c.Created == date.Date);

            return textCons;
        }

        

       

        public async Task<List<Consultation>> GetTodaysAsync(DateTime date,string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user== null)
            {
                return await dbContext.Consultations.Where(p => p.Created.Date == date.Date).ToListAsync();
            }

            return await dbContext.Consultations.Where(p=>p.Created.Date==date.Date && p.Owner_Id==userId).ToListAsync();

    
        }

        public async Task<List<Consultation>> GetTextAsync(string? id, DateTime? date, 
            bool? today,bool? done,Guid? consId)
        {
            var query = dbContext.Consultations
                .Where(c => c.Type_Id == 1)
                .Include(c => c.ConsultationType)
                .AsQueryable();
            
            if (id != null)
            {
                var owner = await userManager.FindByIdAsync(id);

                if (owner == null)
                {
                    throw new Exception("The owner is not found");
                }

                query = query.Where(c => c.Owner_Id == id);
            }

            if (today.HasValue && !today.Value)
            {
                if (date == null)
                {
                    throw new ArgumentException("Date cannot be null when today is false.");
                }

                query = query.Where(c => c.ConsultationStart.HasValue && c.ConsultationStart.Value.Date == date.Value.Date);
            }

            if (today.GetValueOrDefault(true))
            {
                var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

                query = query.Where(c => c.ConsultationStart.HasValue && c.ConsultationStart.Value.Date == localTime.Date);
            }

            if (done != null)
            {
                if (done == true)
                {
                    query = query.Where(c => c.Done == true);
                }
                else
                {
                    query = query.Where(c => c.Done == false);
                }
            }

            if(consId != null)
            {
                query = query.Where(c=>c.Id == consId);
            }

            var cons = await query.ToListAsync();
            return cons;
        }

        public async Task<List<Consultation>> GetVideoAsync(string? id,DateTime? date,bool? done)
        {
            var query = dbContext.Consultations.Where(c => c.Type_Id == 2).Include(c => c.ConsultationType).AsQueryable();

            if (id != null)
            {
                var owner = await userManager.FindByIdAsync(id);

                if (owner == null)
                {
                    throw new Exception("Owner not found");
                }

                query = query.Where(c => c.Owner_Id == id);
            }

            if(date != null)
            {
                var startDate = date.Value.Date;
                var endDate = startDate.AddDays(1);
                query = query.Where(c => c.ConsultationStart >= startDate && c.ConsultationStart <endDate);
            }

            if(done != null)
            {
                if (done == true)
                {
                    query = query.Where(c=>c.Done == true);
                }
                else
                {
                    query = query.Where(c => c.Done == false);
                }
            }
            var cons = await query.ToListAsync();
            return cons;
        }


        public async Task<List<Consultation>> GetEmergencyAsync(string? id, DateTime? date,bool? done)
        {
            var query = dbContext.Consultations.Where(c => c.Type_Id == 3).Include(c => c.ConsultationType).AsQueryable();

            if(id != null)
            {
                var owner = await userManager.FindByIdAsync(id);

                if(owner == null)
                {
                    throw new Exception("Owner not found");
                }

                query = query.Where(c => c.Owner_Id == id);
            }

            if (done != null)
            {
                if (done == true)
                {
                    query = query.Where(c => c.Done == true);
                }
                else
                {
                    query = query.Where(c => c.Done == false);
                }
            }
            var cons = await query.ToListAsync();
            return cons;
        }

        public async Task<Consultation> UpdateStatusAsync(UpdateTextConsStatusRequestDto model)
        {
            var textCons = await dbContext.Consultations.FindAsync(model.Id);

            if(textCons==null)
            {
                throw new Exception("Text consultation is not found");
            }

            textCons.Done = model.Done;

            await dbContext.SaveChangesAsync();

            return textCons;
        }

        public async Task<Consultation> StartConsultationAsync(Guid consId, string userId)
        {
            var cons = await dbContext.Consultations.FindAsync(consId);

            if (cons == null)
            {
                throw new Exception("The consultation is not found");
            }
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("The user is not found");
            }

            cons.Started = true;
            cons.StartedBy = userId;

            await dbContext.SaveChangesAsync();
            return cons;
        }

        
    }
}
