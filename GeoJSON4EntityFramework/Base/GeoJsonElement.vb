Public MustInherit Class GeoJsonElement
    <JsonProperty(PropertyName:="type", Order:=1)>
    Public ReadOnly Property TypeName As String
        Get
            Return [GetType].Name
        End Get
    End Property

    Public Function Serialize(Optional prettyPrint As Boolean = False) As String
        Return GeoJsonSerializer.Serialize(Me, prettyPrint)
    End Function

    Public Function Serialize(settings As JsonSerializerSettings) As String
        Return GeoJsonSerializer.Serialize(Me, settings)
    End Function
End Class