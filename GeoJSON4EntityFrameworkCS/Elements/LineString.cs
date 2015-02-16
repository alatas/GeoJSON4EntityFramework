using System;
using System.Threading.Tasks;
using GeoJSON4EntityFramework.Base;
using Newtonsoft.Json;

namespace GeoJSON4EntityFramework.Elements
{
    public class LineString : GeoJsonGeometry<LineString>, IGeoJsonGeometry
    {

        [JsonIgnore]
        public CoordinateList Points { get; set; }

        public override object Coordinates
        {
            get {
			try {
				switch (Points.Count)
				{
				    case 0:
				        return new double[0];
				    case 1:
				        throw new Exception("There must be an array of two or more points");
				    default:
				    {
				        var @out = new double[Points.Count][];
				        Parallel.For(0, Points.Count, i => { @out[i] = Points[i].Coordinates; });
				        return @out;
				    }
				}
			} catch (Exception) {
				return new double[0];
			}
		}
        }

        public override void CreateFromDbGeometry(DbGeometryWrapper inp)
        {
            if (inp.Geometry.SpatialTypeName != TypeName)
                throw new ArgumentException();
            Points.Clear();

            for (int i = 1; i <= inp.Geometry.PointCount; i++)
            {
                dynamic point = inp.Geometry.PointAt(i);
                Points.AddNew(point.XCoordinate, point.YCoordinate);
            }
        }

    }

}
