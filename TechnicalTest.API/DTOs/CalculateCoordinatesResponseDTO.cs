using TechnicalTest.Core.Models;

namespace TechnicalTest.API.DTOs
{
    public class CalculateCoordinatesResponseDTO
    {
        public List<Coordinate> Coordinates { get; set; }
        public string GridValue { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        //public class Coordinate
        //{
        //    public int X { get; set; }
        //    public int Y { get; set; }

        //    public Coordinate(int x, int y)
        //    {
        //        X = x;
        //        Y = y;
        //    }
        //}

        public CalculateCoordinatesResponseDTO(List<Coordinate> coordinateList, string gridValue)
        {
            Coordinates = coordinateList;
            GridValue = gridValue.ToUpper();
            Message = "Success";
            StatusCode = 200;
        }
    }
}
