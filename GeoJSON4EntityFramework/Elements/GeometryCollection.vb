Imports alatas.GeoJSON4EntityFramework

Partial Public Class GeometryCollection
    Inherits GeoJsonGeometry(Of GeometryCollection)
    Implements IGeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of IGeoJsonGeometry)

    <JsonIgnore>
    Public Overrides ReadOnly Property Coordinates()

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property
End Class
