using System;
using System.Collections.Generic;
using GeoJSON4EntityFramework.Base;
using Newtonsoft.Json;

namespace GeoJSON4EntityFramework.Elements
{
    public class MultiLineString : GeoJsonGeometry<MultiLineString>, IGeoJsonGeometry
    {

        [JsonIgnore]
        public List<LineString> LineStrings { get; set; }

        public override object Coordinates
        {
            get
            {
                //TODO: Complete
                throw new NotImplementedException();
            }
        }

        public override void CreateFromDbGeometry(DbGeometryWrapper inp)
        {
            if (inp.Geometry.SpatialTypeName != TypeName)
                throw new ArgumentException();
            LineStrings.Clear();

            for (int i = 1; i <= inp.Geometry.ElementCount; i++)
            {
                dynamic element = inp.Geometry.ElementAt(i);
                if (element.SpatialTypeName != "LineString")
                    throw new ArgumentException();
                LineStrings.Add((LineString)LineString.FromDbGeometry(new DbGeometryWrapper(element)));
            }
        }

    }
}
