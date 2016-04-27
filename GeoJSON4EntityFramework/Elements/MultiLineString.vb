Imports alatas.GeoJSON4EntityFramework

Public Class MultiLineString
    Inherits GeoJsonGeometry(Of MultiLineString)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property LineStrings As New List(Of LineString)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Return (From ls In LineStrings Let c = ls.Coordinates Select c).ToArray
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_BoundingBox As Double() Implements IGeoJsonGeometry.BoundingBox
        Get
            Return Me.BoundingBox
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        Dim mls As New MultiLineString()
        If Not Me.LineStrings Is Nothing Then
            mls.LineStrings.AddRange(Me.LineStrings.Select(Function(ls) ls.Transform(xform)))
        End If
        Return mls
    End Function
End Class
