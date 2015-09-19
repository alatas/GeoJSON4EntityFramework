Public MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    <JsonProperty(PropertyName:="coordinates")>
    Public MustOverride ReadOnly Property Coordinates() As Object

    <JsonProperty(PropertyName:="bbox", Order:=5, NullValueHandling:=NullValueHandling.Ignore)>
    Public Property BoundingBox As Double()

    <JsonIgnore>
    Public Property WithBoundingBox As Boolean = False
End Class