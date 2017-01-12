Partial Public Class GeometryCollection
    Inherits GeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of GeoJsonGeometry)

    <JsonIgnore>
    Public Overrides ReadOnly Property Coordinates()

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim gc As New GeometryCollection()
        If Not Geometries Is Nothing Then
            gc.Geometries.AddRange(Geometries.Select(Function(g) g.Transform(xform)))
        End If

        If Not BoundingBox Is Nothing Then
            gc.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return gc
    End Function
End Class
