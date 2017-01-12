Public Class CoordinateList
    Inherits List(Of Coordinate)

    Sub AddNew(X As Double, Y As Double)
        MyBase.Add(New Coordinate(X, Y))
    End Sub

    Function CloneList(xform As CoordinateTransform) As CoordinateList
        Dim cloned As New CoordinateList()
        cloned.AddRange([Select](Function(coord) TransformFunctions.TransformCoordinate(coord, xform)))
        Return cloned
    End Function

End Class