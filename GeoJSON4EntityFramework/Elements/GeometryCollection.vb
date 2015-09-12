Partial Public Class GeometryCollection
    Inherits GeoJsonGeometry(Of GeometryCollection)
    Implements IGeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of IGeoJsonGeometry)

    <JsonIgnore>
    Public Overrides ReadOnly Property Coordinates()
End Class
