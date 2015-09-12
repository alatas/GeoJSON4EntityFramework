Public Class MultiPolygon
    Inherits GeoJsonGeometry(Of MultiPolygon)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property Polygons As New List(Of Polygon)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Dim result As New List(Of Object)()
            For Each poly In Polygons
                result.Add(poly.Coordinates)
            Next
            Return result
        End Get
    End Property
End Class