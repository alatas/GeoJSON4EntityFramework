Partial Class Feature

    Public Shared Function FromDbGeometry(inp As System.Data.Entity.Spatial.DbGeometry) As Feature
        Dim f As New Feature

        Select Case inp.SpatialTypeName
            Case "MultiPolygon"
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inp))
            Case "Polygon"
                f.Geometry.Add(Polygon.FromDbGeometry(inp))
            Case "Point"
                f.Geometry.Add(Point.FromDbGeometry(inp))
            Case "MultPoint"
                f.Geometry.Add(MultiPoint.FromDbGeometry(inp))
            Case "LineString"
                f.Geometry.Add(LineString.FromDbGeometry(inp))
            Case "MultiLineString"
                f.Geometry.Add(MultiLineString.FromDbGeometry(inp))
            Case "GeometryCollection"
                f.Geometry.Add(GeometryCollection.FromDbGeometry(inp))
            Case Else
                Throw New NotImplementedException
        End Select

        Return f
    End Function
End Class