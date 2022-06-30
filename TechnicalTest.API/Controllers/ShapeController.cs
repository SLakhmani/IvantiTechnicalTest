using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnicalTest.API.DTOs;
using TechnicalTest.Core;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.API.Controllers
{
    /// <summary>
    /// Shape Controller which is responsible for calculating coordinates and grid value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor of the Shape Controller.
        /// </summary>
        /// <param name="shapeFactory"></param>
        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody] CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            GridValue gridValue;
            Grid grid;
            ShapeEnum shapeType;

            CalculateCoordinatesResponseDTO calculateCoordinatesResponse = new();

            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            if (calculateCoordinatesRequest.ShapeType == (int)ShapeEnum.None)
            {
                calculateCoordinatesResponse.Details = ErrorCodes.GetTriangleError("T100");
                return BadRequest(calculateCoordinatesResponse);         
            }

            // If input string is not in the form '[Row(A-F)][Column(1-12)]', return BadRequest
            try
            {
                gridValue = new GridValue(calculateCoordinatesRequest.GridValue);
            }
            catch (FormatException)
            {
                calculateCoordinatesResponse.Details = ErrorCodes.GetTriangleError("T101");
                return BadRequest(calculateCoordinatesResponse);
            }

            grid = new Grid(calculateCoordinatesRequest.Grid.Size);
            shapeType = (ShapeEnum)calculateCoordinatesRequest.ShapeType;

            // TODO: Call the Calculate function in the shape factory.
            var triangleCoordinates = _shapeFactory.CalculateCoordinates(shapeType, grid, gridValue);

            if (triangleCoordinates == null)
            {
                // TODO: Return BadRequest with error message if the calculate result is null
                calculateCoordinatesResponse.Details = ErrorCodes.GetTriangleError("T102");
                return BadRequest(calculateCoordinatesResponse);
            }

            // TODO: Create ResponseModel with Coordinates and return as OK with responseModel
            calculateCoordinatesResponse = new(triangleCoordinates.Coordinates, calculateCoordinatesRequest.GridValue);
            return Ok(calculateCoordinatesResponse);
        }

        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody]CalculateGridValueDTO gridValueRequest)
        {
            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            Grid grid;
            ShapeEnum shapeType;

            CalculateGridValueResponseDTO gridValueResponse = new();

            if (gridValueRequest.ShapeType == (int)ShapeEnum.None)
            {
                gridValueResponse.Details = ErrorCodes.GetTriangleError("T100");
                return BadRequest(gridValueResponse);
            }

            if (gridValueRequest.ShapeType != (int)ShapeEnum.Triangle)
            {
                gridValueResponse.Details = ErrorCodes.GetTriangleError("T102");
                return BadRequest(gridValueResponse);
            }

            // TODO: Create new Shape with coordinates based on the parameters from the DTO.
            List<Coordinate> triangleCoordinates = new();

            for(int i = 0; i < gridValueRequest.Vertices.Count; i++)
            {
                Coordinate vertex = new(gridValueRequest.Vertices[i].x, gridValueRequest.Vertices[i].y);
                triangleCoordinates.Add(vertex);
            }

            Shape triangleFromCoordinates = new(triangleCoordinates);

            // TODO: Call the function in the shape factory to calculate grid value.
            grid = new Grid(gridValueRequest.Grid.Size);
            shapeType = (ShapeEnum)gridValueRequest.ShapeType;

            GridValue? triangleGridValue;

            // TODO: Return BadRequest with error message if the calculate result is null/invalid
            try
            {
                triangleGridValue = _shapeFactory.CalculateGridValue(shapeType, grid, triangleFromCoordinates);
            }
            catch
            {
                gridValueResponse.Details = ErrorCodes.GetTriangleError("T103");
                return BadRequest(gridValueResponse);
            }

            // TODO: If the GridValue result is null then return BadRequest with an error message.
            if (triangleGridValue == null)
            {     
                gridValueResponse.Details = ErrorCodes.GetTriangleError("T104");
                return BadRequest(gridValueResponse);
            }

            // TODO: Generate a ResponseModel based on the result and return it in Ok();
            gridValueResponse = new(triangleGridValue.Row, triangleGridValue.Column);
            return Ok(gridValueResponse);
        }
    }
}
