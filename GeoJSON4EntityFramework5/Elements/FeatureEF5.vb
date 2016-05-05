Partial Class Feature

    Public Shared Function FromDbGeometry(inp As Spatial.DbGeometry, Optional withBoundingBox As Boolean = False) As Feature
        Dim f As New Feature

        Select Case inp.SpatialTypeName
            Case GeometryType.MultiPolygon
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.Polygon
                f.Geometry.Add(Polygon.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.Point
                f.Geometry.Add(Point.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.MultiPoint
                f.Geometry.Add(MultiPoint.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.LineString
                f.Geometry.Add(LineString.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.MultiLineString
                f.Geometry.Add(MultiLineString.FromDbGeometry(inp, withBoundingBox))
            Case GeometryType.GeometryCollection
                f.Geometry.Add(GeometryCollection.FromDbGeometry(inp, withBoundingBox))
            Case Else
                Throw New NotImplementedException(String.Format("Geometry type not handled: {0}", inp.SpatialTypeName))
        End Select

        Return f
    End Function

    Public Shared Function FromDbGeography(inp As Spatial.DbGeography, Optional withBoundingBox As Boolean = False) As Feature
        Dim f As New Feature

        Dim inpgeom = Spatial.DbSpatialServices.Default.GeometryFromBinary(inp.AsBinary, inp.CoordinateSystemId)

        Select Case inpgeom.SpatialTypeName
            Case GeometryType.MultiPolygon
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.Polygon
                f.Geometry.Add(Polygon.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.Point
                f.Geometry.Add(Point.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.MultiPoint
                f.Geometry.Add(MultiPoint.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.LineString
                f.Geometry.Add(LineString.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.MultiLineString
                f.Geometry.Add(MultiLineString.FromDbGeometry(inpgeom, withBoundingBox))
            Case GeometryType.GeometryCollection
                f.Geometry.Add(GeometryCollection.FromDbGeometry(inpgeom, withBoundingBox))
            Case Else
                Throw New NotImplementedException(String.Format("Geography type not handled: {0}", inp.SpatialTypeName))
        End Select

        Return f
    End Function
End Class