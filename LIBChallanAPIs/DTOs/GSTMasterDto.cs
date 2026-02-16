namespace LIBChallanAPIs.DTOs
{
    public class GSTMasterDto
    {
        public int Id { get; set; }

        public string? GSTId { get; set; }
        public string? GSTSlabId { get; set; }

        public string? GSTTypeId { get; set; }

        public decimal GSTPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }
    }
}
