Partial Class MultiPoint

    Public Overloads Overrides Sub CreateFromDbGeometry(inp As Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> "Point" Then Throw New ArgumentException
            Points.Add(Point.FromDbGeometry(element))
        Next
    End Sub
End Class