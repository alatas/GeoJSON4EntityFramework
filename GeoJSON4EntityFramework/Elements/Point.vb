Public Class Point
    Inherits GeoJsonGeometry(Of Point)
    Implements IGeoJsonGeometry

    Public Property Point As New Coordinate(0, 0)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Return Point.Coordinate
        End Get
    End Property
End Class