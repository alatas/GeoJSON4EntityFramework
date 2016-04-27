Imports alatas.GeoJSON4EntityFramework

Public Class MultiPoint
    Inherits GeoJsonGeometry(Of MultiPoint)
    Implements IGeoJsonGeometry

    <JsonIgnore>
    Public Property Points As New List(Of Point)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            If Points.Count = 0 Then
                Return New Double() {}
            Else
                Dim out(Points.Count - 1)() As Double

                Parallel.For(0, Points.Count, Sub(i)
                                                  out(i) = Points(i).Coordinates
                                              End Sub)
                Return out
            End If
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
        Dim mpt As New MultiPoint()
        If Not Me.Points Is Nothing Then
            mpt.Points.AddRange(Me.Points.Select(Function(pt) pt.Transform(xform)))
        End If
        Return mpt
    End Function
End Class