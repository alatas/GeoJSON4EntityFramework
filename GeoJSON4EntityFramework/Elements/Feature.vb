Public Class Feature
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="id", Order:=2)>
    Public Property ID As String

    <JsonProperty(PropertyName:="properties", Order:=3)>
    Public Property Properties As New Dictionary(Of String, Object)

    <JsonProperty(PropertyName:="geometry", Order:=4)>
    <JsonConverter(GetType(GenericListConverter(Of GeoJsonGeometry)))>
    Public Property Geometry As New List(Of GeoJsonGeometry)

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ParamArray Geometries() As GeoJsonGeometry)
        MyBase.New()
        Geometry = Geometries.ToList
    End Sub
End Class