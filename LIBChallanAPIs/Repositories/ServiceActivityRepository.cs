//using LIBChallanAPIs.DTOs;
//using LIBChallanAPIs.IRepositories;
//using LIBChallanAPIs.Models;
//using Microsoft.EntityFrameworkCore;

//namespace LIBChallanAPIs.Repositories
//{
//    public class ServiceActivityRepository : IServiceActivityRepository
//    {
//        private readonly AppDbContext _context;

//        public ServiceActivityRepository(AppDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<string> CreateAsync(ServiceActivityCreateDto dto, string loggedInUserId)
//        {
//            using var transaction = await _context.Database.BeginTransactionAsync();

//            try
//            {
//                // 🔹 Generate Activity ID
//                var lastActivityId = await _context.ServiceActivities
//                    .Where(x => x.ActivityId.StartsWith("SAI"))
//                    .OrderByDescending(x => x.ActivityId)
//                    .Select(x => x.ActivityId)
//                    .FirstOrDefaultAsync();

//                int nextActivityNumber = 1;

//                if (!string.IsNullOrEmpty(lastActivityId))
//                {
//                    var numberPart = lastActivityId.Substring(3);
//                    if (int.TryParse(numberPart, out int lastNumber))
//                        nextActivityNumber = lastNumber + 1;
//                }

//                string newActivityId = $"SAI{nextActivityNumber:D3}";

//                // 🔹 Create Parent Activity
//                var activity = new ServiceActivity
//                {
//                    ActivityId = newActivityId,
//                    ActivityEngineerId = loggedInUserId,
//                    EntityId = dto.EntityId,
//                    WarehouseId = dto.WarehouseId,
//                    ActivityAddressId = dto.ActivityAddressId,
//                    StatusId = dto.StatusId,
//                    ActivityDate = dto.ActivityDate,
//                    NumberOfBatteriesOnSite = dto.NumberOfBatteriesOnSite,
//                    NumberOfBatteriesOnSiteRTF = dto.NumberOfBatteriesOnSiteRTF,
//                    IsActive = true,
//                    CreatedBy = loggedInUserId,
//                    CreatedAt = DateTime.UtcNow
//                };

//                _context.ServiceActivities.Add(activity);
//                await _context.SaveChangesAsync();

//                // 🔹 Generate Starting Battery ID
//                var lastBatteryId = await _context.BatteryTrans
//                    .Where(x => x.BatteryTransId.StartsWith("TBI"))
//                    .OrderByDescending(x => x.BatteryTransId)
//                    .Select(x => x.BatteryTransId)
//                    .FirstOrDefaultAsync();

//                int nextBatteryNumber = 1;

//                if (!string.IsNullOrEmpty(lastBatteryId))
//                {
//                    var numberPart = lastBatteryId.Substring(3);
//                    if (int.TryParse(numberPart, out int lastNumber))
//                        nextBatteryNumber = lastNumber + 1;
//                }

//                // 🔹 Create Child Batteries
//                foreach (var batteryDto in dto.Batteries)
//                {
//                    string newBatteryId = $"TBI{nextBatteryNumber:D3}";

//                    var battery = new BatteryTran
//                    {
//                        BatteryTransId = newBatteryId,
//                        ActivityId = newActivityId,
//                        BatterySerial = batteryDto.BatterySerial,
//                        BatteryIdInLIBSystem = batteryDto.BatteryIdInLIBSystem,
//                        Barcode = batteryDto.Barcode,
//                        CurrentStatusId = batteryDto.CurrentStatusId,
//                        CustomerId = dto.EntityId,
//                        WarehouseId = dto.WarehouseId,
//                        IsActive = true,
//                        CreatedAt = DateTime.UtcNow
//                    };

//                    _context.BatteryTrans.Add(battery);

//                    nextBatteryNumber++;
//                }

//                await _context.SaveChangesAsync();
//                await transaction.CommitAsync();

//                return newActivityId;
//            }
//            catch
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }

//    }

//    }
