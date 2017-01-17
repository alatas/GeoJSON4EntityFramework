''' <summary>
''' Thrown when there is a failure in transforming a geometry instance
''' </summary>
Public Class TransformException
    Inherits Exception

    Public Sub New(msg As String)
        MyBase.New(msg)
    End Sub

    Public Sub New(msg As String, x As Double, y As Double, tx As Double, ty As Double)
        MyBase.New(msg)
        ParamX = x
        ParamY = y
        ParamTX = tx
        ParamTY = ty
    End Sub

    Public Property ParamX As Double

    Public Property ParamY As Double

    Public Property ParamTX As Double

    Public Property ParamTY As Double
End Class
