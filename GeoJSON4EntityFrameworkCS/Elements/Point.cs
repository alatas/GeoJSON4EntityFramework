using System;
using GeoJSON4EntityFramework.Base;

namespace GeoJSON4EntityFramework.Elements
{
    public class Point : GeoJsonGeometry<Point>, IGeoJsonGeometry
    {
        public Coordinate PointInstance { get; set; }

        public override object Coordinates
        {
            get { return PointInstance.Coordinates; }
        }

        public override void CreateFromDbGeometry(DbGeometryWrapper inp)
        {
            if (inp.Geometry.SpatialTypeName != base.TypeName)
                throw new ArgumentException();
            PointInstance = new Coordinate(inp.Geometry.XCoordinate ?? 0, inp.Geometry.YCoordinate ?? 0);
        }
    }
}
