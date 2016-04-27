Imports alatas.GeoJSON4EntityFramework

Public Class Point
    Inherits GeoJsonGeometry(Of Point)
    Implements IGeoJsonGeometry

    Public Property Point As New Coordinate(0, 0)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Return Point.Coordinate
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If
        Dim pt = New Point()
        If Not Me.Point Is Nothing Then
            pt.Point = Me.Point.Transform(xform)
        End If
        Return pt
    End Function
End Class