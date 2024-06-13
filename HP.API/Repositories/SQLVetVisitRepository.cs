using HP.API.Data;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HP.API.Repositories
{
    public class SQLVetVisitRepository : IVetVisitRepository
    {
        private readonly HPDbContext dbContext;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public SQLVetVisitRepository(HPDbContext dbContext,UserManager<User> userManager,IEmailSender emailSender)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }


        public async Task<VetVisit> CreateAsync(AddVetVisitRequestDto addVetVisitRequestDto)
        {
            var availableSlots = await GetAvailableSlotsAsync(addVetVisitRequestDto.DateTimeStart);
            var desiredSlot = addVetVisitRequestDto.DateTimeStart;
            
            if (!availableSlots.Any(slot => slot.Time.Date == desiredSlot.Date &&
            slot.Time.Hour == desiredSlot.Hour && slot.Time.Minute == desiredSlot.Minute))
            {
                throw new Exception("The time is not free");
            }

            var pet = await dbContext.Pets.FindAsync(addVetVisitRequestDto.Pet_Id);
            var owner = await userManager.FindByIdAsync(addVetVisitRequestDto.Owner_Id);
            var vet = await userManager.FindByIdAsync(addVetVisitRequestDto.Vet_Id);
            if(vet == null)
            {
                throw new Exception("Vet missing");
            }
            if (pet == null || owner == null)
            {
                throw new Exception("Pet or owner missing");
            }
            if (pet.Owner_Id != owner.Id)
            {
                throw new Exception("Pet doesnt match with owner");
            }

            desiredSlot = desiredSlot.AddSeconds(-desiredSlot.Second).AddMilliseconds(-desiredSlot.Millisecond);

            var hour = desiredSlot.Hour;
            var day = desiredSlot.DayOfWeek;
            DateTime today = DateTime.Today;

            if(desiredSlot.Date < today.Date) {
                throw new Exception("The entered date is passed and invalid");
            }
            if(day==DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                throw new Exception("The vet visit cannot be during the weekend");
            }
            if(hour<8 || hour > 20)
            {
                throw new Exception("The visit must be in the working hours");
            }

            var vetVisit = new VetVisit()
            {
                Pet_Id = addVetVisitRequestDto.Pet_Id,
                Pet_Name = pet.Name,
                Owner_Id = addVetVisitRequestDto.Owner_Id,
                Owner_Name = owner.DisplayName,
                DateTimeStart = desiredSlot,
                DateTimeEnd = desiredSlot.AddMinutes(30),
                Created = addVetVisitRequestDto.Created,
                Approved = false,
                Vet_Id = addVetVisitRequestDto.Vet_Id,
                Vet_Name = vet.Id,
                Owner_Email = owner.Email
            };

            await dbContext.VetVisits.AddAsync(vetVisit);
            await dbContext.SaveChangesAsync();

            return vetVisit;
        }

        public async Task<VetVisit> DeleteAsync(Guid id, string message)
        {
            var vetVisit = await dbContext.VetVisits.FindAsync(id);

            if (vetVisit == null)
            {
                throw new Exception("Vet visit not found");
            };

            try
            {
                var userEmail = vetVisit.Owner_Email;
                //await emailSender.SendEmailAsync(userEmail,"Одбиен преглед", message);
                await emailSender.SendEmailAsync("cetohic946@bizatop.com", "Одбиен преглед", message);
            }
            catch(Exception ex)
            {
                throw new Exception("The email has a problem sending");
            }
            
            
            dbContext.VetVisits.Remove(vetVisit);
            await dbContext.SaveChangesAsync();

            return vetVisit;
            
        }

        public async Task<List<VetVisit>> GetAllAsync(FilterVetVisitDto filterVetVisitDto)
        {
            var vetVisits = dbContext.VetVisits.AsQueryable();
            
            if(filterVetVisitDto.filterDate.HasValue)
            {
                var date = filterVetVisitDto.filterDate.Value.Date;
                vetVisits = vetVisits.Where(visit=>visit.DateTimeStart.Date == date);
            }

            if(!string.IsNullOrWhiteSpace(filterVetVisitDto.filterStatus))
            {
                var current = DateTime.Now;
               if(filterVetVisitDto.filterStatus.Equals("Idni",StringComparison.OrdinalIgnoreCase))
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart > current);
                }
                else if (filterVetVisitDto.filterStatus.Equals("Pominati", StringComparison.OrdinalIgnoreCase))
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart < current);
                }
                

            }

            if (filterVetVisitDto.filterMonth.HasValue)
            {
                var month = filterVetVisitDto.filterMonth.Value;
                if (month == 1)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 1);
                }
                else if (month == 2)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 2);
                }
                else if (month == 3)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 3);
                }
                else if (month == 4)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 4);
                }
                else if (month == 5)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 5);
                }
                else if (month == 6)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 6);
                }
                else if (month == 7)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 7);
                }
                else if (month == 8)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 8);
                }
                else if (month == 9)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 9);
                }
                else if (month == 10)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 10);
                }
                else if (month == 11)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 11);
                }
                else if (month == 12)
                {
                    vetVisits = vetVisits.Where(visit => visit.DateTimeStart.Month == 12);
                }
                else
                {
                    return null;
                }
            }

            if(!string.IsNullOrWhiteSpace(filterVetVisitDto.filterQuery))
            {
                vetVisits = vetVisits.Where(visit=>visit.Pet_Name.Contains(filterVetVisitDto.filterQuery) || 
                visit.Owner_Name.Contains(filterVetVisitDto.filterQuery));
            }

            return await vetVisits.ToListAsync();
        }


        public async Task<List<Slot>> GetAllSlotsAsync(DateTime date)
        {
            var slots = new List<Slot>();

            var startWorkTime = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            var endWorkTime = startWorkTime.AddHours(12);

            var vetVisits = await dbContext.VetVisits.ToListAsync();

            var bookedSlots = new HashSet<DateTime>();

            foreach (var vetVisit in vetVisits)
            {
                var visitStartTime = vetVisit.DateTimeStart;
                var visitEndTime = vetVisit.DateTimeEnd;

                while (visitStartTime < visitEndTime)
                {
                    bookedSlots.Add(visitStartTime);
                    visitStartTime = visitStartTime.AddMinutes(30);
                }
            }

            while (startWorkTime < endWorkTime)
            {
                if (startWorkTime.Hour >= 8 && startWorkTime.Hour < 20)
                {
                    var isAvailable = !bookedSlots.Contains(startWorkTime);
                    slots.Add(new Slot { Time = startWorkTime, isAvailable = isAvailable, SlotType = SlotType.VetVisit});
                }
                startWorkTime = startWorkTime.AddMinutes(30);
            }

            return slots;
        }

        public async Task<List<Slot>> GetAvailableSlotsAsync(DateTime date)
        {
        
            var slots = new List<Slot>();

            var startWorkTime = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            var endTime = startWorkTime.AddHours(12);

            var vetVisits = await dbContext.VetVisits.ToListAsync();

            var bookedSlots = new HashSet<DateTime>();

            foreach (var vetVisit in vetVisits)
            {
                var visitStartTime = vetVisit.DateTimeStart;
                var visitEndTime = vetVisit.DateTimeEnd;

                while (visitStartTime < visitEndTime)
                {
                    bookedSlots.Add(visitStartTime);
                    visitStartTime = visitStartTime.AddMinutes(30);
                }
            }

            while (startWorkTime < endTime)
            {
                if (startWorkTime.Hour >= 8 && startWorkTime.Hour < 20 && !bookedSlots.Contains(startWorkTime))
                {
                    slots.Add(new Slot { Time = startWorkTime, isAvailable = true });
                }
                startWorkTime = startWorkTime.AddMinutes(30);
            }

            return slots;
        }

        public async Task<List<VetVisit>> GetByUserAsync(string userId)
        {
            var vetVisits = await dbContext.VetVisits.Where(visit => visit.Owner_Id == userId).ToListAsync();

            return vetVisits;
        }

        public async Task<Slot> GetTimeByIdAsync(Guid id)
        {
            var slot = await dbContext.Slots.FindAsync(id);

            if(slot == null)
            {
                throw new Exception("The time slot is not found");
            }

            return slot;
        }

        public async Task<VetVisit> UpdateAsync(UpdateVetVisitRequestDto updateVetVisitRequestDto)
        {
            var vetVisit = await dbContext.VetVisits.FindAsync(updateVetVisitRequestDto.Id);
            var vet = await userManager.FindByIdAsync(updateVetVisitRequestDto.Vet_Id);

            if (vet == null)
            {
                return null;
            }

            if(vetVisit== null)
            {
                return null;
            }
            
            if (updateVetVisitRequestDto.IsApproved != null) {
                vetVisit.Approved = updateVetVisitRequestDto.IsApproved.Value;
                if(updateVetVisitRequestDto.IsApproved == true)
                {
                    try
                    {
                        var dateAndTime = vetVisit.DateTimeStart;
                        
                        var year = dateAndTime.Year;
                        var month = dateAndTime.Month;
                        var day = dateAndTime.Day;
                        var hour = dateAndTime.Hour;
                        var minute = dateAndTime.Minute;
                        var userEmail = vetVisit.Owner_Email;
                        
                        await emailSender.SendEmailAsync("cetohic946@bizatop.com", "Одобрен преглед", 
                            "Вашето барање за преглед во "+ hour + ":" + minute + " на датум " + month+"/"+day+"/"+year +
                               " со "+vet.DisplayName +" е одобрено. Ве очекуваме!");
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Problem sending the email");
                    }
                }
            }

            if (updateVetVisitRequestDto.DateTimeStart.HasValue)
            {
                vetVisit.DateTimeStart = updateVetVisitRequestDto.DateTimeStart.Value;
            }
            if (!string.IsNullOrWhiteSpace(updateVetVisitRequestDto.Vet_Id))
            {
                vetVisit.Vet_Id = updateVetVisitRequestDto.Vet_Id;
                vetVisit.Vet_Name = vet.DisplayName;
            }
            await dbContext.SaveChangesAsync();

            return vetVisit;

        }
    }
}

