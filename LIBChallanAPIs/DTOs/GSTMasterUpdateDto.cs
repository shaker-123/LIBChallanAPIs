namespace LIBChallanAPIs.DTOs
{
    public class GSTMasterUpdateDto
    {
        public decimal? GSTPercentage { get; set; }
        public int Id { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool? IsActive { get; set; }
    }
}
