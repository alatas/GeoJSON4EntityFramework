#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class Polygon
    Inherits GeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Rings As New List(Of CoordinateList)

    Public Overrides ReadOnly Property Coordinates()
        Get
            Return Rings
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim poly As New Polygon()
        If Not Rings Is Nothing Then
            poly.Rings.AddRange(Rings.Select(Function(ring) ring.CloneList(xform)))
        End If

        If Not BoundingBox Is Nothing Then
            poly.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return poly
    End Function

    Private Function RingToCoordinateList(ring As DbGeometry) As CoordinateList
        Dim extRingCoords As New CoordinateList()
        For i = 1 To ring.PointCount
            Dim pt = ring.PointAt(i)
            extRingCoords.Add(New Coordinate(pt.XCoordinate, pt.YCoordinate))
        Next
        Return extRingCoords
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
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