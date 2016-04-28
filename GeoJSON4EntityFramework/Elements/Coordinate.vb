<JsonConverter(GetType(CoordinateConverter))>
Public Class Coordinate
    Sub New()
        MyBase.New()
    End Sub

    Sub New(_x As Double, _y As Double)
        X = _x
        Y = _y
    End Sub

    Friend Function Transform(ByVal xform As CoordinateTransform) As Coordinate
        Dim tx As Double
        Dim ty As Double
        If Not xform(Me.X, Me.Y, tx, ty) Then
            Throw New TransformException("Failed to transform coordinate")
        End If
        Return New Coordinate(tx, ty)
    End Function

    Friend Shared Function TransformBoundingBox(ByVal coords As Double(), ByVal xform As CoordinateTransform) As Double()
        If coords Is Nothing Then
            Throw New ArgumentNullException(NameOf(coords))
        End If
        If coords.Length <> 4 Then
            Throw New ArgumentException("The given bounding box array does not have 4 coordinate values")
        End If
        Dim ll As New Coordinate(coords(1), coords(0))
        Dim ur As New Coordinate(coords(3), coords(2))
        Dim tll = ll.Transform(xform)
        Dim tur = ur.Transform(xform)
        Return New Double() {
            tll.Y,
            tll.X,
            tur.Y,
            tur.X
        }
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