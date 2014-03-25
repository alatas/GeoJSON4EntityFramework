Public Class FeatureCollection
    Inherits GeoJsonElement(Of FeatureCollection)

    <JsonConverter(GetType(GenericListConverter(Of Feature)))>
    <JsonProperty(PropertyName:="features")>
    Public Property Features As New List(Of Feature)
End Class