namespace TechnicalTest.API.DTOs
{
    public class CalculateGridValueResponseDTO
    {
        public CalculateGridValueResponseDTO(string? row, int column)
        {
            Row = row;
            Column = column;
            Message = "Success";
            StatusCode = 200;
        }

        public string Row { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}
