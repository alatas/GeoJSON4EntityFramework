Partial Public Class GeometryCollection
    Public Overrides Sub CreateFromDbGeometry(inp As Spatial.DbGeometry)
        If inp.SpatialTypeName <> "GeometryCollection" Then Throw New ArgumentException
        Geometries.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            Select Case element.SpatialTypeName
                Case "MultiPolygon"
                    Geometries.Add(MultiPolygon.FromDbGeometry(element, WithBoundingBox))
                Case "Polygon"
                    Geometries.Add(Polygon.FromDbGeometry(element, WithBoundingBox))
                Case "Point"
                    Geometries.Add(Point.FromDbGeometry(element, WithBoundingBox))
                Case "MultiPoint"
                    Geometries.Add(MultiPoint.FromDbGeometry(element, WithBoundingBox))
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
    End Sub
End Class
