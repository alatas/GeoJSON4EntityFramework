Imports System.Reflection
Imports Newtonsoft.Json.Linq

Public Class GeoJsonGeometryConverter
    Inherits JsonConverter

    Private Shared _supportedTypes() As Type

    Shared Sub New()
        _supportedTypes = GetType(GeoJsonElement).Assembly.GetTypes().Where(Function(f) f.IsSubclassOf(GetType(GeoJsonElement))).ToArray()
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim token = JObject.Load(reader)
        Dim typeName = token("type")
        Dim typeInstance = _supportedTypes.Where(Function(f) f.Name = typeName).First()
        Dim instance = serializer.Deserialize(token.CreateReader(), typeInstance)
        Return instance
    End Function

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return _supportedTypes.Contains(objectType)
    End Function

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Throw New NotImplementedException()
    End Sub

End Class
