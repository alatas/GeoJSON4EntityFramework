Public Class TransformFunctions
    Public Shared Function TransformCoordinate(inp As Coordinate, ByVal xform As CoordinateTransform) As Coordinate
        Dim tx As Double
        Dim ty As Double

        If Not xform(inp.X, inp.Y, tx, ty) Then
            Throw New TransformException("Failed to transform coordinate", inp.X, inp.Y, tx, ty)
        End If

        Return New Coordinate(tx, ty)
    End Function

    Public Shared Function TransformBoundingBox(ByVal coords As Double(), ByVal xform As CoordinateTransform) As Double()
        If coords Is Nothing Then
            Throw New ArgumentNullException(NameOf(coords))
        End If

        If coords.Length <> 4 Then
            Throw New ArgumentException("The given bounding box array does not have 4 coordinate values")
        End If

        Dim tll = TransformCoordinate(New Coordinate(coords(1), coords(0)), xform)
        Dim tur = TransformCoordinate(New Coordinate(coords(3), coords(2)), xform)

        Return New Double() {
            tll.Y,
            tll.X,
            tur.Y,
            tur.X
        }
    End Function
End Class

''' <summary>
''' A function that transforms the given coordinate
''' </summary>
''' <param name="x"></param>
''' <param name="y"></param>
''' <param name="tx"></param>
''' <param name="ty"></param>
''' <returns></returns>
Public Delegate Function CoordinateTransform(ByVal x As Double, ByVal y As Double, ByRef tx As Double, ByRef ty As Double) As Boolean
