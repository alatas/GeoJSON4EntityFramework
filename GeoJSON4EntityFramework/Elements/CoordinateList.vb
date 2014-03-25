Public Class CoordinateList
    Inherits List(Of Coordinate)

    Sub AddNew(X As Double, Y As Double)
        MyBase.Add(New Coordinate(X, Y))
    End Sub
End Class