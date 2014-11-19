Partial Class Polygon
    Public Overrides Sub CreateFromDbGeometry(inp As Data.Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        PointsList.Clear()

        Ring2Coordinates(inp.ExteriorRing)
        Dim numRings = inp.InteriorRingCount
        If (numRings = 0) Then Return
        For i As Integer = 1 To numRings
            Dim ring = inp.InteriorRingAt(i)
            Ring2Coordinates(ring)
        Next

    End Sub

    Private Sub Ring2Coordinates(ring As Data.Entity.Spatial.DbGeometry)
        Dim Points = New CoordinateList
        For i As Integer = 1 To ring.PointCount
            Dim point = ring.PointAt(i)
            Points.AddNew(point.XCoordinate, point.YCoordinate)
        Next
        PointsList.Add(Points)
    End Sub

End Class