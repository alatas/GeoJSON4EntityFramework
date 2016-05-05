Partial Public Class GeometryCollection
    Public Overrides Sub CreateFromDbGeometry(inp As Spatial.DbGeometry)
        If inp.SpatialTypeName <> GeometryType.GeometryCollection Then Throw New ArgumentException
        Geometries.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            Select Case element.SpatialTypeName
                Case GeometryType.MultiPolygon
                    Geometries.Add(MultiPolygon.FromDbGeometry(element, WithBoundingBox))
                Case GeometryType.Polygon
                    Geometries.Add(Polygon.FromDbGeometry(element, WithBoundingBox))
                Case GeometryType.Point
                    Geometries.Add(Point.FromDbGeometry(element, WithBoundingBox))
                Case GeometryType.MultiPoint
                    Geometries.Add(MultiPoint.FromDbGeometry(element, WithBoundingBox))
                Case GeometryType.LineString
                    Geometries.Add(LineString.FromDbGeometry(element, WithBoundingBox))
                Case GeometryType.MultiLineString
                    Geometries.Add(MultiLineString.FromDbGeometry(element, WithBoundingBox))
                Case Else
                    Throw New NotImplementedException(String.Format("Geometry type not handled: {0}", inp.SpatialTypeName))
            End Select
        Next
    End Sub
End Class
