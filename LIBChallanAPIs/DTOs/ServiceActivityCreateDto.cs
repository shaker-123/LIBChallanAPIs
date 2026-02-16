namespace LIBChallanAPIs.DTOs
{
    public class ServiceActivityCreateDto
    {
        public string EntityId { get; set; } = null!;
        public string WarehouseId { get; set; } = null!;
        public string ActivityAddressId { get; set; } = null!;
        public string StatusId { get; set; } = null!;

     
        public DateTime ActivityDate { get; set; }

        public int NumberOfBatteriesOnSite { get; set; }
        public int NumberOfBatteriesOnSiteRTF { get; set; }

        public List<BatteryCreateDto> Batteries { get; set; } = new();
    }
}
