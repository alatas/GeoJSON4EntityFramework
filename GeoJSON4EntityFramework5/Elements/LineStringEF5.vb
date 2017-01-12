Partial Class LineString
    Public Overrides Sub CreateFromDbGeometry(inp As Spatial.DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.PointCount
            Dim point = inp.PointAt(i)
            Points.AddNew(point.XCoordinate, point.YCoordinate)
        Next
    End Sub
End Class
