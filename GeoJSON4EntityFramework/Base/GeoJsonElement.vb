Public MustInherit Class GeoJsonElement
    <JsonProperty(PropertyName:="type", Order:=1)>
    Public ReadOnly Property TypeName As String
        Get
            Return [GetType].Name
        End Get
    End Property
End Class