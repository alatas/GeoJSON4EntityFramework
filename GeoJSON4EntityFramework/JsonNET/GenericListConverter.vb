Public Class GenericListConverter(Of T)
    Inherits Newtonsoft.Json.JsonConverter

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Throw New NotImplementedException
    End Function

    Public Overrides Function ReadJson(reader As Newtonsoft.Json.JsonReader, objectType As Type, existingValue As Object, serializer As Newtonsoft.Json.JsonSerializer) As Object
        Throw New NotImplementedException
    End Function

    Public Overrides Sub WriteJson(writer As Newtonsoft.Json.JsonWriter, value As Object, serializer As Newtonsoft.Json.JsonSerializer)
        Dim list As List(Of T) = value
        If list.Count = 0 Then
            serializer.Serialize(writer, Nothing)
        ElseIf list.Count = 1 Then
            serializer.Serialize(writer, list.FirstOrDefault)
        Else
            serializer.Serialize(writer, list)
        End If
    End Sub
End Class
