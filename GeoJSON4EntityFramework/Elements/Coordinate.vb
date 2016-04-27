<JsonConverter(GetType(CoordinateConverter))>
Public Class Coordinate
    Sub New()
        MyBase.New()
    End Sub

    Sub New(_x As Double, _y As Double)
        X = _x
        Y = _y
    End Sub

    Friend Function Transform(xform As CoordinateTransform) As Coordinate
        Dim tx As Double
        Dim ty As Double
        If Not xform(Me.X, Me.Y, tx, ty) Then
            Throw New TransformException("Failed to transform coordinate")
        End If
        Return New Coordinate(tx, ty)
    End Function

    Public ReadOnly Property Coordinate As Double()
        Get
            Return New Double() {X, Y}
        End Get
    End Property

    <JsonIgnore()>
    Public Property X As Double

    <JsonIgnore()>
    Public Property Y As Double

End Class