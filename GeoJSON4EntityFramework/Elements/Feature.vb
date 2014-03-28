Public Class Feature
    Inherits GeoJsonElement(Of Feature)
    <JsonProperty(PropertyName:="id", Order:=2)>
    Public Property ID As String

    <JsonProperty(PropertyName:="properties", Order:=3)>
    Public Property Properties As New Dictionary(Of String, String)

    <JsonProperty(PropertyName:="geometry", Order:=4)>
    <JsonConverter(GetType(GenericListConverter(Of IGeoJsonGeometry)))>
    Public Property Geometry As New List(Of IGeoJsonGeometry)

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ParamArray Geometries() As IGeoJsonGeometry)
        MyBase.New()
        Geometry = Geometries.ToList
    End Sub
End Class