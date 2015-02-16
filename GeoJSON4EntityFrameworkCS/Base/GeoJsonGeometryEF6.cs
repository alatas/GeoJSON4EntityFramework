using System.Data.Entity.Spatial;

namespace GeoJSON4EntityFramework.Base
{
    public abstract partial class GeoJsonGeometry<T>
    {
        public static GeoJsonGeometry<T> FromDbGeometry(DbGeometry inp)
        {
            return FromDbGeometry(new DbGeometryWrapper(inp));
        }
    }
}
