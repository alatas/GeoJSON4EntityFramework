Public MustInherit Class GeoJsonGeometry
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="coordinates")>
    Public MustOverride ReadOnly Property Coordinates() As Object

    <JsonProperty(PropertyName:="bbox", Order:=5, NullValueHandling:=NullValueHandling.Ignore)>
    Public Property BoundingBox As Double()

    <JsonIgnore>
    Public Property WithBoundingBox As Boolean = False

    Public MustOverride Function Transform(xform As CoordinateTransform) As GeoJsonGeometry

End Class