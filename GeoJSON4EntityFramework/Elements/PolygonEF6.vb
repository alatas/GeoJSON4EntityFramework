Partial Class Polygon
<<<<<<< HEAD
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
=======

    Private Function RingToCoordinateList(ring As Entity.Spatial.DbGeometry) As CoordinateList
        Dim extRingCoords As New CoordinateList()
        For i = 1 To ring.PointCount
            Dim pt = ring.PointAt(i)
            extRingCoords.Add(New Coordinate(pt.XCoordinate, pt.YCoordinate))
        Next
        Return extRingCoords
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Rings.Clear()

        ' Process exterior ring
        Dim extRing = inp.ExteriorRing
        Rings.Add(RingToCoordinateList(extRing))

        ' Process interior rings (ie. holes)
        If inp.InteriorRingCount > 0 Then
            For i = 1 To inp.InteriorRingCount
                Dim intRing = inp.InteriorRingAt(i)
                Rings.Add(RingToCoordinateList(intRing))
            Next
        End If
>>>>>>> additions_2
    End Sub

End Class