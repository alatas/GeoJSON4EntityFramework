Partial Class MultiPolygon
    Public Overrides Sub CreateFromDbGeometry(inp As Data.Spatial.DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Polygons.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> GeometryType.Polygon Then Throw New ArgumentException
            Polygons.Add(FromDbGeometry(element))
        Next
    End Sub
End Class