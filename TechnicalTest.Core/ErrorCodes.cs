namespace TechnicalTest.Core
{
    public class ErrorCodes
    {
        public static Dictionary<string, string> TriangleErrors { get; } = new()
        {
            { "T100", "No 'ShapeType' Selected. 'ShapeType' must be 1 for Triangle" },
            { "T101", "'GridValue' must be in the form [Row(A-F)][Column(1-12)], For Eg. 'A2'" },
            { "T102", "'ShapeType' not yet implemented. Try 'ShapeType': 1 for Triangle" },
            { "T103", "Coordinates incompatible with entered Grid Size" },
            { "T104", "Triangle must have 3 Coordinates"},
        };

        public static string GetTriangleError(string errorCode)
        {
            return TriangleErrors[errorCode];
        }

    }
}
