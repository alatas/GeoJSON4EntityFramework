Public Class Point
    Inherits GeoJsonGeometry

    <JsonIgnore()>
    Public Property Point As New Coordinate(0, 0)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Return Point.Coordinate
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim pt = New Point()
        If Not Point Is Nothing Then
            pt.Point = TransformFunctions.TransformCoordinate(Point, xform)
        End If

        If Not BoundingBox Is Nothing Then
            pt.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return pt
    End Function
End Class