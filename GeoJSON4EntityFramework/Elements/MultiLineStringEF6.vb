Partial Class MultiLineString
    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        LineStrings.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> GeometryType.LineString Then Throw New ArgumentException
            LineStrings.Add(FromDbGeometry(element))
        Next
    End Sub
End Class
