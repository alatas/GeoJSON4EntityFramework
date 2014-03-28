Partial Class Polygon
    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.PointCount
            Dim point = inp.PointAt(i)
            Points.AddNew(point.XCoordinate, point.YCoordinate)
        Next
    End Sub
End Class