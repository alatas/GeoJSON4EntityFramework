
Public Class MultiLineString
    Inherits GeoJsonGeometry

    <JsonIgnore()>
    Public Property LineStrings As New List(Of LineString)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Return (From ls In LineStrings Let c = ls.Coordinates Select c).ToArray
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim mls As New MultiLineString()
        If Not LineStrings Is Nothing Then
            mls.LineStrings.AddRange(LineStrings.Select(Function(ls) CType(ls.Transform(xform), LineString)))
        End If

        If Not Me.BoundingBox Is Nothing Then
            mls.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return mls
    End Function
End Class
