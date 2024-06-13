using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HP.API.Data;
using HP.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HP.API.BackgroundServices
{
    public class ConsultationUpdaterService : BackgroundService
    {
        private readonly ILogger<ConsultationUpdaterService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsultationUpdaterService(ILogger<ConsultationUpdaterService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("TextConsultation background service is running.");
                Console.WriteLine("******************Consultation background service is running");
                // Check and update text consultations
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<HPDbContext>();
                    var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

                    var allConsultations = await dbContext.Consultations.ToListAsync();
                    var videoConsultationsToUpdate = allConsultations.Where(c => !c.Done && (c.Type_Id == 2 || c.Type_Id == 3) && c.ConsultationStart.HasValue && 
                    localTime > c.ConsultationStart.Value.AddMinutes(30)).ToList();
                    if (videoConsultationsToUpdate.Any()) 
                    {
                        foreach (var consultation in videoConsultationsToUpdate)
                        {
                            Console.WriteLine($"&&&&&&&&&&&&&&&&& vreme  {localTime}");
                            Console.WriteLine($"********************************{consultation.Id} set to done");
                            consultation.Done = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"&&&&&&&&&&&&&&&&& vreme  {localTime}");
                        Console.WriteLine("****************************Nema bate");
                    }
                   

                    await dbContext.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); 
            }

            _logger.LogInformation("TextConsultation background service has stopped running.");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TextConsultation background service is starting.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TextConsultation background service is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
