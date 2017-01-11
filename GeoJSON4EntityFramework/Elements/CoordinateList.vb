Public Class CoordinateList
    Inherits List(Of Coordinate)

    Sub AddNew(X As Double, Y As Double)
        MyBase.Add(New Coordinate(X, Y))
    End Sub

    Friend Function CloneList(xform As CoordinateTransform) As CoordinateList
        Dim cloned As New CoordinateList()
        cloned.AddRange(Me.Select(Function(coord) coord.Transform(xform)))
        Return cloned
    End Function

End Class