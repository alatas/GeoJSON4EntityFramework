Public Class GeoJsonSerializer
    Public Shared Function Serialize(inp As GeoJsonElement, Optional prettyPrint As Boolean = False) As String
        Dim settings As New JsonSerializerSettings
        settings.ContractResolver = New OrderedContractResolver
        settings.NullValueHandling = NullValueHandling.Ignore
        settings.FloatFormatHandling = FloatFormatHandling.DefaultValue
        settings.FloatParseHandling = FloatParseHandling.Double

        If prettyPrint Then
            settings.Formatting = Formatting.Indented
        End If

        Return JsonConvert.SerializeObject(inp, settings)
    End Function

    Public Shared Function Serialize(inp As GeoJsonElement, settings As JsonSerializerSettings) As String
        Return JsonConvert.SerializeObject(inp, settings)
    End Function
End Class
