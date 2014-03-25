Public Class GeoJsonSerializer
    Public Shared Function Serialize(Of T)(inp As GeoJsonElement(Of T), Optional prettyPrint As Boolean = False) As String
        Dim settings As New Newtonsoft.Json.JsonSerializerSettings
        settings.ContractResolver = New OrderedContractResolver
        settings.NullValueHandling = NullValueHandling.Ignore
        settings.FloatFormatHandling = FloatFormatHandling.DefaultValue
        settings.FloatParseHandling = FloatParseHandling.Double

        If prettyPrint Then
            settings.Formatting = Formatting.Indented
        End If

        Return JsonConvert.SerializeObject(inp, settings)
    End Function
End Class
