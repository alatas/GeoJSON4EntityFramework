Public Class MultiLineString
    Inherits GeoJsonGeometry(Of MultiLineString)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property LineStrings As New List(Of LineString)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            'TODO: Complete
            Throw New NotImplementedException
        End Get
    End Property
End Class
