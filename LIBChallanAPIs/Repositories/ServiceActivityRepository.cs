using LIBChallanAPIs.Comman;
using LIBChallanAPIs.DTOs;
using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBChallanAPIs.Repositories
{
    public class ServiceActivityRepository : IServiceActivityRepository
    {
        private readonly AppDbContext _context;

        public ServiceActivityRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateAsync(ServiceActivityCreateDto dto, string loggedInUserId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var lastActivityId = await _context.ServiceActivities
                    .Where(x => x.ActivityId.StartsWith("SAI"))
                    .OrderByDescending(x => x.ActivityId)
                    .Select(x => x.ActivityId)
                    .FirstOrDefaultAsync();

                int nextActivityNumber = 1;

                if (!string.IsNullOrEmpty(lastActivityId))
                {
                    var numberPart = lastActivityId.Substring(3);
                    if (int.TryParse(numberPart, out int lastNumber))
                        nextActivityNumber = lastNumber + 1;
                }

                string newActivityId = $"SAI{nextActivityNumber:D3}";

                var activity = new ServiceActivity
                {
                    ActivityId = newActivityId,
                    ActivityEngineerId = loggedInUserId,
                    EntityId = dto.EntityId,
                    WarehouseId = dto.WarehouseId,
                    StatusId = dto.StatusId,
                    ActivityDate = dto.ActivityDate,
                    NumberOfBatteriesOnSite = dto.NumberOfBatteriesOnSite,
                    NumberOfBatteriesOnSiteRTF = dto.NumberOfBatteriesOnSiteRTF,
                    IsActive = true,
                    CreatedBy = loggedInUserId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.ServiceActivities.Add(activity);
                await _context.SaveChangesAsync();

                var lastBatteryId = await _context.BatteryTrans
                    .Where(x => x.BatteryTransId.StartsWith("TBI"))
                    .OrderByDescending(x => x.BatteryTransId)
                    .Select(x => x.BatteryTransId)
                    .FirstOrDefaultAsync();

                int nextBatteryNumber = 1;

                if (!string.IsNullOrEmpty(lastBatteryId))
                {
                    var numberPart = lastBatteryId.Substring(3);
                    if (int.TryParse(numberPart, out int lastNumber))
                        nextBatteryNumber = lastNumber + 1;
                }

                foreach (var batteryDto in dto.Batteries)
                {
                    string newBatteryId = $"TBI{nextBatteryNumber:D3}";

                    var battery = new BatteryTran
                    {
                        BatteryTransId = newBatteryId,
                        ActivityId = newActivityId,
                        BatterySerial = batteryDto.BatterySerial,
                        BatteryIdInLIBSystem = batteryDto.BatteryIdInLIBSystem,
                        Barcode = batteryDto.Barcode,
                        CurrentStatusId = batteryDto.CurrentStatusId,
                        CustomerId = dto.EntityId,
                        WarehouseId = dto.WarehouseId,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.BatteryTrans.Add(battery);

                    nextBatteryNumber++;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return newActivityId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateSingleBatteryAsync(string activityId, string batterySerial, BatteryTranUpdateDto batteryDto)
        {
            var battery = await _context.BatteryTrans
                .FirstOrDefaultAsync(b => b.ActivityId == activityId && b.BatterySerial == batterySerial);

            if (battery == null)
                throw new KeyNotFoundException($"Battery with serial '{batterySerial}' for Activity '{activityId}' not found.");

            battery.BatteryIdInLIBSystem = batteryDto.BatteryIdInLIBSystem;
            battery.Barcode = batteryDto.Barcode;
            battery.CurrentStatusId = batteryDto.CurrentStatusId;
            battery.WarehouseId = batteryDto.WarehouseId;
            battery.CustomerId = batteryDto.CustomerId;
            battery.IsActive = batteryDto.IsActive;
            battery.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<ServiceActivityDto>> GetPagedActivitiesAsync(PagedRequest request)
        {
            IQueryable<ServiceActivity> query = _context.ServiceActivities
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.BatteryStatus)
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.Customer)
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.Warehouse)
                .Include(a => a.Warehouse)
                .Include(a => a.Entity)
                .Include(a => a.Status)
                .Include(a => a.ActivityEngineer);

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                string search = request.SearchValue.ToLower();
                query = query.Where(a =>
                    a.ActivityId.ToLower().Contains(search) ||
                    a.Entity.EntityName.ToLower().Contains(search) ||
                    a.Warehouse.WarehouseCode.ToLower().Contains(search) ||
                    a.ActivityEngineer.UserName.ToLower().Contains(search));
            }

            switch (request.Status)
            {
                case RecordStatus.Active:
                    query = query.Where(a => a.IsActive);
                    break;
                case RecordStatus.Inactive:
                    query = query.Where(a => !a.IsActive);
                    break;
                case RecordStatus.All:
                default:
                    break;
            }

            var totalRecords = await query.CountAsync();

            if (!string.IsNullOrEmpty(request.SortColumn))
            {
                if (request.SortColumn.Equals("ActivityDate", StringComparison.OrdinalIgnoreCase))
                    query = request.SortDirection.ToUpper() == "DESC" ? query.OrderByDescending(a => a.ActivityDate)
                                                                       : query.OrderBy(a => a.ActivityDate);
                else
                    query = query.OrderBy(a => a.ActivityId); 
            }
            else
            {
                query = query.OrderBy(a => a.ActivityId);
            }

            var activities = await query
                .Skip(request.Skip)
                .Take(request.PageSize)
                .ToListAsync();

            var data = activities.Select(a => new ServiceActivityDto
            {
                ActivityId = a.ActivityId,
                ActivityEngineerId = a.ActivityEngineerId,
                ActivityEngineerName = a.ActivityEngineer?.UserName,
                EntityId = a.EntityId,
                EntityName = a.Entity?.EntityName,
                WarehouseId = a.WarehouseId,
                WarehouseName = a.Warehouse?.WarehouseCode,
                StatusId = a.StatusId,
                StatusName = a.Status?.StatusName,
                ActivityDate = a.ActivityDate,
                NumberOfBatteriesOnSite = a.NumberOfBatteriesOnSite,
                NumberOfBatteriesOnSiteRTF = a.NumberOfBatteriesOnSiteRTF,
                IsActive = a.IsActive,
                Batteries = a.Batteries.Select(b => new BatteryTranDto
                {
                    BatteryTransId = b.BatteryTransId,
                    BatterySerial = b.BatterySerial,
                    BatteryIdInLIBSystem = b.BatteryIdInLIBSystem,
                    Barcode = b.Barcode,
                    CurrentStatusId = b.CurrentStatusId,
                    CurrentStatusName = b.BatteryStatus?.StatusName,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer?.EntityName,
                    WarehouseId = b.WarehouseId,
                    WarehouseName = b.Warehouse?.WarehouseCode,
                    IsActive = b.IsActive
                }).ToList()
            }).ToList();

            return new PagedResponse<ServiceActivityDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }


        public async Task<ServiceActivityDto?> GetActivityByIdAsync(string activityId)
        {
            var activity = await _context.ServiceActivities
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.BatteryStatus) 
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.Customer)     
                .Include(a => a.Batteries)
                    .ThenInclude(b => b.Warehouse)    
                .Include(a => a.Warehouse)            
                .Include(a => a.Entity)               
                .Include(a => a.Status)               
                .Include(a => a.ActivityEngineer)     
                .FirstOrDefaultAsync(a => a.ActivityId == activityId);

            if (activity == null)
                return null;

            return new ServiceActivityDto
            {
                ActivityId = activity.ActivityId,
                ActivityEngineerId = activity.ActivityEngineerId,
                ActivityEngineerName = activity.ActivityEngineer?.UserName,
                EntityId = activity.EntityId,
                EntityName = activity.Entity?.EntityName,
                WarehouseId = activity.WarehouseId,
                WarehouseName = activity.Warehouse?.WarehouseCode,
                StatusId = activity.StatusId,
                StatusName = activity.Status?.StatusName,
                ActivityDate = activity.ActivityDate,
                NumberOfBatteriesOnSite = activity.NumberOfBatteriesOnSite,
                NumberOfBatteriesOnSiteRTF = activity.NumberOfBatteriesOnSiteRTF,
                IsActive = activity.IsActive,
                Batteries = activity.Batteries.Select(b => new BatteryTranDto
                {
                    BatteryTransId = b.BatteryTransId,
                    BatterySerial = b.BatterySerial,
                    BatteryIdInLIBSystem = b.BatteryIdInLIBSystem,
                    Barcode = b.Barcode,
                    CurrentStatusId = b.CurrentStatusId,
                    CurrentStatusName = b.BatteryStatus?.StatusName,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer?.EntityName,
                    WarehouseId = b.WarehouseId,
                    WarehouseName = b.Warehouse?.WarehouseCode,
                    IsActive = b.IsActive
                }).ToList()
            };
        }

        public async Task<List<ServiceActivityDto>> GetActivitiesByUserIdAsync(string userId)
        {

            var activities = await _context.ServiceActivities
         .Where(a => a.ActivityEngineerId == userId)
         .Include(a => a.Batteries)
         .Include(a => a.Warehouse)
         .Include(a => a.Entity)
         .Include(a => a.Status)
         .Include(a => a.ActivityEngineer)
         .ToListAsync();

            return activities.Select(a => new ServiceActivityDto
            {
                ActivityId = a.ActivityId,
                ActivityEngineerId = a.ActivityEngineerId,
                ActivityEngineerName = a.ActivityEngineer?.UserName, 
                EntityId = a.EntityId,
                EntityName = a.Entity?.EntityName,                  
                WarehouseId = a.WarehouseId,
                WarehouseName = a.Warehouse?.WarehouseCode,          
                StatusId = a.StatusId,
                StatusName = a.Status?.StatusName,                 
                ActivityDate = a.ActivityDate,
                NumberOfBatteriesOnSite = a.NumberOfBatteriesOnSite,
                NumberOfBatteriesOnSiteRTF = a.NumberOfBatteriesOnSiteRTF,
                IsActive = a.IsActive,
                Batteries = a.Batteries.Select(b => new BatteryTranDto
                {
                    BatteryTransId = b.BatteryTransId,
                    BatterySerial = b.BatterySerial,
                    BatteryIdInLIBSystem = b.BatteryIdInLIBSystem,
                    Barcode = b.Barcode,
                    CurrentStatusId = b.CurrentStatusId,
                    CurrentStatusName = b.BatteryStatus?.StatusName, 
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer?.EntityName,        
                    WarehouseId = b.WarehouseId,
                    WarehouseName = b.Warehouse?.WarehouseCode,
                    IsActive = b.IsActive
                }).ToList()
            }).ToList();
        }
    }

}
