using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoJSON4EntityFramework.Base;

namespace GeoJSON4EntityFramework.Elements
{
    public class MultiPoint : GeoJsonGeometry<MultiPoint>, IGeoJsonGeometry
    {

        public List<Point> Points { get; set; }

        public override object Coordinates
        {
            get {
			if (Points.Count == 0) {
				return new double[0];
			} else {
				var @out = new double[Points.Count][];

				Parallel.For(0, Points.Count, i => { @out[i] = (double[])Points[i].Coordinates; });
				return @out;
			}
		}
        }

        public override void CreateFromDbGeometry(DbGeometryWrapper inp)
        {
            if (inp.Geometry.SpatialTypeName != TypeName)
                throw new ArgumentException();
            Points.Clear();

            for (int i = 1; i <= inp.Geometry.ElementCount; i++)
            {
                dynamic element = inp.Geometry.ElementAt(i);
                if (element.SpatialTypeName != "Point")
                    throw new ArgumentException();
                Points.Add((Point)Point.FromDbGeometry(new DbGeometryWrapper(element)));
            }
        }

    }

}
