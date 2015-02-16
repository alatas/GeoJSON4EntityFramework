Public Class MultiPoint
    Inherits GeoJsonGeometry(Of MultiPoint)
    Implements IGeoJsonGeometry

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

    Public Overloads Overrides Sub CreateFromDbGeometry(inp As DbGeometryWrapper)
        If inp.Geometry.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.Geometry.ElementCount
            Dim element = inp.Geometry.ElementAt(i)
            If element.SpatialTypeName <> "Point" Then Throw New ArgumentException
            Points.Add(Point.FromDbGeometry(New DbGeometryWrapper(element)))
        Next
    End Sub

End Class