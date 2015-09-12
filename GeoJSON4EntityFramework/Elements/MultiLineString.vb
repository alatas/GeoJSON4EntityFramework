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
End Class
