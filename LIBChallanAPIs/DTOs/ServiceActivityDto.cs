public class ServiceActivityDto
{
    public string ActivityId { get; set; } = null!;
    public string ActivityEngineerId { get; set; } = null!;
    public string? ActivityEngineerName { get; set; }       // new
    public string EntityId { get; set; } = null!;
    public string? EntityName { get; set; }               // new
    public string WarehouseId { get; set; } = null!;
    public string? WarehouseName { get; set; }            // new
    public string StatusId { get; set; } = null!;
    public string? StatusName { get; set; }               // new
    public DateTime ActivityDate { get; set; }
    public int NumberOfBatteriesOnSite { get; set; }
    public int NumberOfBatteriesOnSiteRTF { get; set; }
    public bool IsActive { get; set; }
    public List<BatteryTranDto> Batteries { get; set; } = new();
}

public class BatteryTranDto
{
    public string BatteryTransId { get; set; } = null!;
    public string BatterySerial { get; set; } = null!;
    public string BatteryIdInLIBSystem { get; set; } = null!;
    public string Barcode { get; set; } = null!;
    public string FirmwareStatusId { get; set; } = string.Empty;
    public string CurrentStatusId { get; set; } = null!;
    public string? CurrentStatusName { get; set; }         // new
    public string CustomerId { get; set; } = null!;
    public string? CustomerName { get; set; }             // new
    public string? FirmwareStatusName { get; set; }
    public string WarehouseId { get; set; } = null!;
    public string? WarehouseName { get; set; }            // new

    public string? CorrectiveActionId { get; set; }
    public string? CorrectiveActionName { get; set; }

    public string? DefectTypeId { get; set; }
    public string? DefectName { get; set; }

    public string? PartId { get; set; }
    public string? PartName { get; set; }
    public bool IsActive { get; set; }
}
