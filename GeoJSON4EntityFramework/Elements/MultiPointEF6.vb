Partial Class MultiPoint
    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> GeometryType.Point Then Throw New ArgumentException
            Points.Add(FromDbGeometry(element))
        Next
    End Sub
End Class