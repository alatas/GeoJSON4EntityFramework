Public Class CoordinateList
    Inherits List(Of Coordinate)

    Sub AddNew(X As Double, Y As Double)
        MyBase.Add(New Coordinate(X, Y))
    End Sub

    Friend Function CloneList(xform As CoordinateTransform) As List(Of Coordinate)
        Dim cloned As New List(Of Coordinate)()
        cloned.AddRange(Me.Select(Function(coord) coord.Transform(xform)))
        Return cloned
    End Function

End Class