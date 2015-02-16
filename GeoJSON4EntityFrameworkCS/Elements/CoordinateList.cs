using System.Collections.Generic;

namespace GeoJSON4EntityFramework.Elements
{
    public class CoordinateList : List<Coordinate>
    {
        public void AddNew(double x, double y)
        {
            Add(new Coordinate(y, y));
        }
    }
}
