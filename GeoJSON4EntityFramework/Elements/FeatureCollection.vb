Public Class FeatureCollection
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="features")>
    Public Property Features As New List(Of Feature)
End Class