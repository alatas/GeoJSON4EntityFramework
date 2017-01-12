Partial Class Feature

    Public Shared Function FromDbGeometry(inp As Entity.Spatial.DbGeometry, Optional withBoundingBox As Boolean = False) As Feature
        Return New Feature(GeoJsonGeometry.FromDbGeometry(inp, withBoundingBox))
    End Function

    Public Shared Function FromDbGeography(inp As Entity.Spatial.DbGeography, Optional withBoundingBox As Boolean = False) As Feature

        Return New Feature(GeoJsonGeometry.FromDbGeometry(Entity.Spatial.DbSpatialServices.Default.GeometryFromBinary(inp.AsBinary, inp.CoordinateSystemId), withBoundingBox))

    End Function
End Class