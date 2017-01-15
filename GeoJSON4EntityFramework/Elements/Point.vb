#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

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

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Point = New Coordinate(inp.XCoordinate, inp.YCoordinate)
    End Sub
End Class