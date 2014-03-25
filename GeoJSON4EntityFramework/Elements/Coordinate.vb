Public Class Coordinate
    Sub New()
        MyBase.New()
    End Sub

    Sub New(_x As Double, _y As Double)
        X = _x
        Y = _y
    End Sub

    Public ReadOnly Property Coordinate As Double()
        Get
            Return New Double() {X, Y}
        End Get
    End Property

    Public Property X As Double

    Public Property Y As Double

End Class