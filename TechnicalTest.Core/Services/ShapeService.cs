using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Services
{
    public class ShapeService : IShapeService
    {
        public Shape ProcessTriangle(Grid grid, GridValue gridValue)
        {
            // TODO: Calculate the coordinates.
         
            List<Coordinate> triangleCoordinates = new List<Coordinate>();
            Coordinate leftTopVertex = new Coordinate(0, 0);
            Coordinate outerVertex = new Coordinate(0, 0);
            Coordinate bottomRightVertex = new Coordinate(0, 0);

            int row = gridValue.GetNumericRow();
            int column = gridValue.Column;
            int columnNumber = (int)Math.Ceiling((decimal)(column / 2.0));
            int size = grid.Size;

            // Left top vertex
            leftTopVertex.Y = (row - 1) * size;
            leftTopVertex.X = (columnNumber - 1) * size;
            triangleCoordinates.Add(leftTopVertex);

            // Outer vertex
            if (column % 2 == 0)
            {
                outerVertex.Y = (row - 1) * size;
                outerVertex.X = columnNumber * size;
                triangleCoordinates.Add(outerVertex);
            }
            else
            {
                outerVertex.Y = row * size;
                outerVertex.X = (columnNumber - 1) * size;
                triangleCoordinates.Add(outerVertex);
            }

            // Bottom right vertex
            bottomRightVertex.Y = row * size;
            bottomRightVertex.X = columnNumber * size;
            triangleCoordinates.Add(bottomRightVertex);

            return new Shape(triangleCoordinates);
        }

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            // TODO: Calculate the grid value.
            int row = triangle.BottomRightVertex.Y / grid.Size;
            int columnNumber = triangle.TopLeftVertex.X / grid.Size + 1;
            int column = 0;

            if (!AreCoordinatesValidate(triangle, grid)) throw new Exception();

            // Identify if right triangle or left triangle
            if (triangle.TopLeftVertex.X == triangle.OuterVertex.X)
            {
                // Left Triangle
                column = columnNumber * 2 - 1;
            }
            if (triangle.BottomRightVertex.X == triangle.OuterVertex.X)
            {
                // Right Triangle
                column = columnNumber * 2;
            }

            return new GridValue(row, column);
        }

        public static bool AreCoordinatesValidate(Triangle triangle, Grid grid)
        {
            // Check if Triangle coordinates and grid size are compatible
            bool coordinatesValid = true;

            var side1Distance = (int)Math.Sqrt((Math.Pow(triangle.TopLeftVertex.X - triangle.OuterVertex.X, 2)
                                                + Math.Pow(triangle.TopLeftVertex.Y - triangle.OuterVertex.Y, 2)));

            var side2Distance = (int)Math.Sqrt((Math.Pow(triangle.BottomRightVertex.X - triangle.OuterVertex.X, 2)
                                                + Math.Pow(triangle.BottomRightVertex.Y - triangle.OuterVertex.Y, 2)));

            if (side1Distance != grid.Size || side2Distance != grid.Size) coordinatesValid = false;

            return coordinatesValid;
        }
    }
}