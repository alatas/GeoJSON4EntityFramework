Partial Class MultiPolygon
    Public Overrides Sub CreateFromDbGeometry(inp As Data.Spatial.DbGeometry)
        If inp.SpatialTypeName <> "MultiPolygon" Then Throw New ArgumentException
        Polygons.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> "Polygon" Then Throw New ArgumentException
            Polygons.Add(Polygon.FromDbGeometry(element))
        Next
    End Sub
End Class