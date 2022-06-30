namespace TechnicalTest.API.DTOs
{
    public class CalculateGridValueResponseDTO
    {
        public string? Row { get; set; }
        public int? Column { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public int StatusCode { get; set; }

        public CalculateGridValueResponseDTO(string? row, int? column)
        {
            Row = row;
            Column = column;
            Message = "Success";
            StatusCode = 200;
        }

        public CalculateGridValueResponseDTO()
        {
            Message = "Failure";
            StatusCode = 400;
        }
    }
}
