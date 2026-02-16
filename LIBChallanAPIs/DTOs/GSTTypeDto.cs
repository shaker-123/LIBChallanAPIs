namespace LIBChallanAPIs.DTOs
{
    public class GSTTypeDto
    {
        public int Id { get; set; }
        public string GSTTypeId { get; set; } = string.Empty;

        public string GSTTypeCode { get; set; } = string.Empty;
        public string GSTTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class GSTTypeCreateDto
    {
        public string GSTTypeCode { get; set; } = string.Empty;
        public string GSTTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class GSTTypeUpdateDto
    {
        public string? GSTTypeCode { get; set; }
        public string? GSTTypeName { get; set; }
    }
}
