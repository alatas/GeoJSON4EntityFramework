Imports System.Runtime.Serialization
''' <summary>
''' Thrown when there is a failure in transforming a geometry instance
''' </summary>
Public Class TransformException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(msg As String)
        MyBase.New(msg)
    End Sub

    Public Sub New(msg As String, inner As Exception)
        MyBase.New(msg, inner)
    End Sub

    Public Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
