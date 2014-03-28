Public MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    <JsonProperty(PropertyName:="coordinates")>
    Public MustOverride ReadOnly Property Coordinates() As Object

End Class