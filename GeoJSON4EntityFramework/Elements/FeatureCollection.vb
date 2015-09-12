Public Class FeatureCollection
    Inherits GeoJsonElement(Of FeatureCollection)

    <JsonProperty(PropertyName:="features")>
    Public Property Features As New List(Of Feature)
End Class