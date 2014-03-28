Public Class MultiPolygon
    Inherits GeoJsonGeometry(Of MultiPolygon)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property Polygons As New List(Of Polygon)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            If Polygons.Count = 0 Then
                Return New Double() {}
            Else
                Dim out(Polygons.Count - 1)()()() As Double

                Parallel.For(0, Polygons.Count, Sub(i)
                                                    out(i) = Polygons(i).Coordinates
                                                End Sub)
                Return out
            End If
        End Get
    End Property
End Class