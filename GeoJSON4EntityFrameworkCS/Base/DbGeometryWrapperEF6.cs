using System.Data.Entity.Spatial;

namespace GeoJSON4EntityFramework.Base
{
    public class DbGeometryWrapper
    {
        public DbGeometry Geometry { get; set; }

        public DbGeometryWrapper(DbGeometry geometry)
        {
            Geometry = geometry;
        }
    }
}
