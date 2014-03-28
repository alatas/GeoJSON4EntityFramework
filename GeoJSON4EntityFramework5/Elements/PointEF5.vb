Partial Class Point
    Public Overrides Sub CreateFromDbGeometry(inp As Data.Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Point = New Coordinate(inp.XCoordinate, inp.YCoordinate)
    End Sub
End Class