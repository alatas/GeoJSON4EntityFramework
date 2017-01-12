Partial Public Class GeometryCollection
    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Geometries.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            Geometries.Add(FromDbGeometry(element, WithBoundingBox))
        Next
    End Sub
End Class
