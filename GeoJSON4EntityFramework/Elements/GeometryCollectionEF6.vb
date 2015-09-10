Partial Public Class GeometryCollection
    Public Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> "GeometryCollection" Then Throw New ArgumentException
        Geometries.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            Select Case element.SpatialTypeName
                Case "MultiPolygon"
                    Geometries.Add(MultiPolygon.FromDbGeometry(element))
                Case "Polygon"
                    Geometries.Add(Polygon.FromDbGeometry(element))
                Case "Point"
                    Geometries.Add(Point.FromDbGeometry(element))
                Case "MultiPoint"
                    Geometries.Add(MultiPoint.FromDbGeometry(element))
            End Select
        Next
    End Sub
End Class
