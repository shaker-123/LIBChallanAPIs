namespace LIBChallanAPIs.DTOs
{
    public class CorrectiveActionDto
    {
        public int Id { get; set; }
        public string ActionId { get; set; } = string.Empty;

        public string ActionName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
