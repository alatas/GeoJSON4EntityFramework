Public Class Polygon
    Inherits GeoJsonGeometry(Of Polygon)
    Implements IGeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Rings As New List(Of CoordinateList)

    Public Overrides ReadOnly Property Coordinates()
        Get
            Return Rings
        End Get
    End Property
End Class