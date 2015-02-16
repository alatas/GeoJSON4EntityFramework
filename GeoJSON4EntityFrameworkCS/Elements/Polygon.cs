using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoJSON4EntityFramework.Base;

namespace GeoJSON4EntityFramework.Elements
{
    public class Polygon : GeoJsonGeometry<Polygon>, IGeoJsonGeometry
    {

        [Newtonsoft.Json.JsonIgnore]
        public List<CoordinateList> PointsList { get; set; }

        public override object Coordinates
        {
            get {
			try {
				if (PointsList.Count == 0) {
					return new double[0];
				} else {
					var @out = new double[PointsList.Count][][];
					for (var k = 0; k <= PointsList.Count - 1; k++)
					{
					    var points3 = PointsList[k];
					    switch (points3.Count)
					    {
					        case 0:
					            return new double[0];
					        case 1:
					            throw new Exception("There must be an array of two or more points");
					        default:
					            @out[k] = new double[points3.Count][];
					            Parallel.For(0, points3.Count, i => { @out[k][i] = points3[i].Coordinates; });
					            break;
					    }
					}
				    return @out;
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
            PointsList.Clear();

            Ring2Coordinates(new DbGeometryWrapper(inp.Geometry.ExteriorRing));
            dynamic numRings = inp.Geometry.InteriorRingCount;
            if ((numRings == 0))
                return;
            for (var i = 1; i <= numRings; i++)
            {
                dynamic ring = inp.Geometry.InteriorRingAt(i);
                Ring2Coordinates(new DbGeometryWrapper(ring));
            }

        }

        private void Ring2Coordinates(DbGeometryWrapper ring)
        {
            dynamic points = new CoordinateList();
            for (var i = 1; i <= ring.Geometry.PointCount; i++)
            {
                dynamic point = ring.Geometry.PointAt(i);
                points.AddNew(point.XCoordinate, point.YCoordinate);
            }
            PointsList.Add(points);
        }

    }

}
