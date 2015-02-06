Partial Class Polygon

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
    End Sub
End Class