Public Class MultiPolygon
    Inherits GeoJsonGeometry(Of MultiPolygon)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property Polygons As New List(Of Polygon)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            If Polygons.Count = 0 Then
                Return New Double() {}
            ElseIf Polygons.Count = 1 Then
                Return Polygons(0).Coordinates
            Else
                Dim out(Polygons.Count - 1)()()() As Double

                Parallel.For(0, Polygons.Count, Sub(i)
                                                    out(i) = Polygons(i).Coordinates
                                                End Sub)
                Return out
            End If
        End Get
    End Property

    Public Overrides Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)
        If inp.SpatialTypeName <> "MultiPolygon" Then Throw New ArgumentException
        Polygons.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> "Polygon" Then Throw New ArgumentException
            Polygons.Add(Polygon.FromDbGeometry(element))
        Next
    End Sub
End Class