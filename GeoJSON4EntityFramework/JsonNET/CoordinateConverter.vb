Imports Newtonsoft.Json.Linq

Public Class CoordinateConverter
    Inherits JsonConverter

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return objectType = GetType(Coordinate)
    End Function

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Dim array As New JArray()
        Dim coord = TryCast(value, Coordinate)
        If Not coord Is Nothing Then
            array.Add(coord.X)
            array.Add(coord.Y)
            array.WriteTo(writer)
        End If
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim coord As New Coordinate()
        Dim array = JArray.Load(reader)
        If (array.Count = 2) Then
            coord.X = array(0).ToObject(Of Double)()
            coord.Y = array(1).ToObject(Of Double)()
        End If
        Return coord
    End Function

End Class
