Public Class Polygon
    Inherits GeoJsonGeometry(Of Polygon)
    Implements IGeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Points As New CoordinateList

    Public Overrides ReadOnly Property Coordinates()
        Get
            Try
                If Points.Count = 0 Then
                    Return New Double() {}
                ElseIf Points.Count = 1 Then
                    Dim out() As Double = New Double() {Points(0).X, Points(0).Y}
                    Return out
                Else
                    Dim out(0)()() As Double
                    out(0) = New Double(Points.Count - 1)() {}
                    Parallel.For(0, Points.Count, Sub(i)
                                                      out(0)(i) = Points(i).Coordinate
                                                  End Sub)
                    Return out
                End If
            Catch ex As Exception
                Return New Double() {}
            End Try
        End Get
    End Property

    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.PointCount
            Dim point = inp.PointAt(i)
            Points.AddNew(point.XCoordinate, point.YCoordinate)
        Next
    End Sub
End Class