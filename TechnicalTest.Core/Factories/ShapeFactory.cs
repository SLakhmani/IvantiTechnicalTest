using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Factories
{
    public class ShapeFactory : IShapeFactory
    {
	    private readonly IShapeService _shapeService;

        public ShapeFactory(IShapeService shapeService)
        {
	        _shapeService = shapeService;
        }

        public Shape? CalculateCoordinates(ShapeEnum shapeEnum, Grid grid, GridValue gridValue)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    // TODO: Return shape returned from service.
                    return _shapeService.ProcessTriangle(grid, gridValue);
                default:
                    return null;
            }
        }

        public GridValue? CalculateGridValue(ShapeEnum shapeEnum, Grid grid, Shape shape)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    if (shape.Coordinates.Count != 3) return null;
                    // TODO: Return grid value returned from service.
                    // In case the order is not specified
                    var sortedEnumerable = shape.Coordinates.OrderBy(coord => coord.X).ThenBy(coord => coord.Y);
                    List<Coordinate> sortedCoordinates = sortedEnumerable.ToList();

                    Triangle triangleFromCoordinates = new(sortedCoordinates[0], sortedCoordinates[1], sortedCoordinates[2]);
                    return _shapeService.ProcessGridValueFromTriangularShape(grid, triangleFromCoordinates);
                default:
                    return null;
            }
        }
    }
}
