using Blazor.Data;
using Microsoft.EntityFrameworkCore;
namespace Blazor.Services
{
    public class DatabaseService
    {

        private UptimeMonitorDbContext dbContext;
        private ILogger<DatabaseService> logger;
        private bool dbIsGood;

        public DatabaseService(UptimeMonitorDbContext dbContext, ILogger<DatabaseService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<Uptime>?> GetMonitorRecordsAsync()
        {
            List<Uptime>? ret = null;
            try
            {
                if (!await VerifyDatabase())
                    return null;

                ret = await dbContext.Uptime.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fejl");
            }
            return ret;
        }

        public async Task<Uptime?> AddMonitorRecordAsync(Uptime rec)
        {
            try
            {
                if (!await VerifyDatabase())
                    return null;

                dbContext.Uptime.Add(rec);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fejl");
            }
            return rec;
        }

        public async Task<Uptime?> UpdateMonitorRecordAsync(Uptime rec)
        {
            try
            {
                if (!await VerifyDatabase())
                    return null;
                var monitorRecordExist = dbContext.Uptime.FirstOrDefault(p => p.Id == rec.Id);
                if (monitorRecordExist != null)
                {
                    dbContext.Update(rec);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fejl");
            }
            return rec;
        }

        public async Task<Uptime?> DeleteMonitorRecordAsync(Uptime rec)
        {
            try
            {
                if (!await VerifyDatabase())
                    return null;

                dbContext.Uptime.Remove(rec);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fejl");
            }
            return rec;
        }

        private async Task<bool> VerifyDatabase()
        {
            var res = false;
            try
            {
                var dbCreated = await dbContext.Database.EnsureCreatedAsync();
                res = dbContext.Database.CanConnect();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fejl");
            }
            return res;
        }

    }
}