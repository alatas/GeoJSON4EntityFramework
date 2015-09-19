Partial Class Feature

    Public Shared Function FromDbGeometry(inp As Entity.Spatial.DbGeometry, Optional withBoundingBox As Boolean = False) As Feature
        Dim f As New Feature

        Select Case inp.SpatialTypeName
            Case "MultiPolygon"
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inp, withBoundingBox))
            Case "Polygon"
                f.Geometry.Add(Polygon.FromDbGeometry(inp, withBoundingBox))
            Case "Point"
                f.Geometry.Add(Point.FromDbGeometry(inp, withBoundingBox))
            Case "MultiPoint"
                f.Geometry.Add(MultiPoint.FromDbGeometry(inp, withBoundingBox))
            Case "LineString"
                f.Geometry.Add(LineString.FromDbGeometry(inp, withBoundingBox))
            Case "MultiLineString"
                f.Geometry.Add(MultiLineString.FromDbGeometry(inp, withBoundingBox))
            Case "GeometryCollection"
                f.Geometry.Add(GeometryCollection.FromDbGeometry(inp, withBoundingBox))
            Case Else
                Throw New NotImplementedException
        End Select

        Return f
    End Function

    Public Shared Function FromDbGeography(inp As Entity.Spatial.DbGeography, Optional withBoundingBox As Boolean = False) As Feature
        Dim f As New Feature

        Dim inpgeom = Entity.Spatial.DbSpatialServices.Default.GeometryFromBinary(inp.AsBinary, inp.CoordinateSystemId)

        Select Case inpgeom.SpatialTypeName
            Case "MultiPolygon"
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inpgeom, withBoundingBox))
            Case "Polygon"
                f.Geometry.Add(Polygon.FromDbGeometry(inpgeom, withBoundingBox))
            Case "Point"
                f.Geometry.Add(Point.FromDbGeometry(inpgeom, withBoundingBox))
            Case "MultiPoint"
                f.Geometry.Add(MultiPoint.FromDbGeometry(inpgeom, withBoundingBox))
            Case "LineString"
                f.Geometry.Add(LineString.FromDbGeometry(inpgeom, withBoundingBox))
            Case "MultiLineString"
                f.Geometry.Add(MultiLineString.FromDbGeometry(inpgeom, withBoundingBox))
            Case "GeometryCollection"
                f.Geometry.Add(GeometryCollection.FromDbGeometry(inpgeom, withBoundingBox))
            Case Else
                Throw New NotImplementedException
        End Select

        Return f
    End Function
End Class