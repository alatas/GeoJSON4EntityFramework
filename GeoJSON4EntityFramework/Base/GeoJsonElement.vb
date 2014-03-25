Public MustInherit Class GeoJsonElement(Of T)
    <JsonProperty(PropertyName:="type", Order:=1)>
    Public ReadOnly Property TypeName As String
        Get
            Return GetType(T).Name
        End Get
    End Property
End Class