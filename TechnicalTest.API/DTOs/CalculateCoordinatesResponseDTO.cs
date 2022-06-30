using TechnicalTest.Core.Models;

namespace TechnicalTest.API.DTOs
{
    public class CalculateCoordinatesResponseDTO
    {
        public List<Coordinate> Coordinates { get; set; }
        public string GridValue { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public int StatusCode { get; set; }

        public CalculateCoordinatesResponseDTO(List<Coordinate> coordinateList, string gridValue)
        {
            Coordinates = coordinateList;
            GridValue = gridValue.ToUpper();
            Message = "Success";
            StatusCode = 200;
        }

        public CalculateCoordinatesResponseDTO()
        {
            Message = "Failure";
            StatusCode = 400;
        }
    }
}
