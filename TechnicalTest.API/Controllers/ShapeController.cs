using Microsoft.AspNetCore.Mvc;
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
            string errMsg;
            GridValue gridValue;
            Grid grid;
            ShapeEnum shapeType;

            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            if (calculateCoordinatesRequest.ShapeType == (int)ShapeEnum.None)
            {
                errMsg = String.Format(@"""ShapeType"": {0} is None, ""ShapeType"" must be 1 for Triangle.", calculateCoordinatesRequest.ShapeType);
                return BadRequest(errMsg);         
            }

            // If input string is not in the form '[Row(A-F)][Column(1-12)]', return BadRequest
            try
            {
                gridValue = new GridValue(calculateCoordinatesRequest.GridValue);
            }
            catch (FormatException)
            {
                errMsg = String.Format(@"""GridValue"": {0} must be in the form [Row(A-F)][Column(1-12)], For Eg. ""A2""", calculateCoordinatesRequest.GridValue);
                return BadRequest(errMsg);
            }

            grid = new Grid(calculateCoordinatesRequest.Grid.Size);
            shapeType = (ShapeEnum)calculateCoordinatesRequest.ShapeType;

            // TODO: Call the Calculate function in the shape factory.

            var triangleCoordinates = _shapeFactory.CalculateCoordinates(shapeType, grid, gridValue);

            if (triangleCoordinates == null)
            {
                // TODO: Return BadRequest with error message if the calculate result is null
                errMsg = String.Format(@"""ShapeType"": {0} is not a triangle, ""ShapeType"" must be 1 for Triangle.", calculateCoordinatesRequest.ShapeType);
                return BadRequest(errMsg);
            }

            // TODO: Create ResponseModel with Coordinates and return as OK with responseModel

            CalculateCoordinatesResponseDTO calculateCoordinatesResponse = new(triangleCoordinates.Coordinates, calculateCoordinatesRequest.GridValue);
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
            string errMsg;
            Grid grid;
            ShapeEnum shapeType;

            if (gridValueRequest.ShapeType == (int)ShapeEnum.None)
            {
                errMsg = String.Format(@"""ShapeType"": {0} is None, ""ShapeType"" must be 1 for Triangle.", gridValueRequest.ShapeType);
                return BadRequest(errMsg);
            }

            if (gridValueRequest.Vertices.Count != 3)
            {
                errMsg = String.Format(@"Cannot Calculate ""GridValue"" for Triangle as ""Vertices.Count"": {0}.", gridValueRequest.Vertices.Count);
                return BadRequest(errMsg);
            }

            // TODO: Create new Shape with coordinates based on the parameters from the DTO.
            List<Coordinate> triangleCoordinates = new List<Coordinate>();

            for(int i = 0; i < gridValueRequest.Vertices.Count; i++ )
            {
                Coordinate vertex = new(gridValueRequest.Vertices[i].x, gridValueRequest.Vertices[i].y);
                triangleCoordinates.Add(vertex);
            }

            Shape triangleFromCoordinates = new(triangleCoordinates);

            // TODO: Call the function in the shape factory to calculate grid value.
            grid = new Grid(gridValueRequest.Grid.Size);
            shapeType = (ShapeEnum)gridValueRequest.ShapeType;

            GridValue? triangleGridValue;

            try
            {
                triangleGridValue = _shapeFactory.CalculateGridValue(shapeType, grid, triangleFromCoordinates);
            }
            catch
            {
                errMsg = String.Format(@"Coordinates incompatible with ""GridSize"": {0}.", grid.Size);
                return BadRequest(errMsg);
            }

            // TODO: If the GridValue result is null then return BadRequest with an error message.
            if (triangleGridValue == null)
            {
                // TODO: Return BadRequest with error message if the calculate result is null
                errMsg = String.Format(@"""ShapeType"": {0} is not triangle, ""ShapeType"" must be 1 for Triangle.", gridValueRequest.ShapeType);
                return BadRequest(errMsg);
            }

            // TODO: Generate a ResponseModel based on the result and return it in Ok();

            CalculateGridValueResponseDTO gridValueResponse = new(triangleGridValue.Row, triangleGridValue.Column);
            return Ok(gridValueResponse);
        }
    }
}
