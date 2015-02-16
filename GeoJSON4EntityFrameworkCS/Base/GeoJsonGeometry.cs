using System;
using Newtonsoft.Json;

namespace GeoJSON4EntityFramework.Base
{
    public abstract partial class GeoJsonGeometry<T> : GeoJsonElement<T>
    {

        [JsonProperty(PropertyName = "coordinates")]
        public abstract object Coordinates { get; }

        [JsonProperty(PropertyName = "bbox", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public double[] BoundingBox { get; set; }

        public abstract void CreateFromDbGeometry(DbGeometryWrapper inp);

        static internal GeoJsonGeometry<T> FromDbGeometry(DbGeometryWrapper inp)
        {
            var obj = Activator.CreateInstance<T>() as GeoJsonGeometry<T>;

            if (obj == null) return null;
            obj.BoundingBox = new[]
            {
                inp.Geometry.Envelope.PointAt(1).YCoordinate ?? 0,
                inp.Geometry.Envelope.PointAt(1).XCoordinate ?? 0,
                inp.Geometry.Envelope.PointAt(3).YCoordinate ?? 0,
                inp.Geometry.Envelope.PointAt(3).XCoordinate ?? 0
            };

            obj.CreateFromDbGeometry(inp);
            return obj;
        }

    }
}
