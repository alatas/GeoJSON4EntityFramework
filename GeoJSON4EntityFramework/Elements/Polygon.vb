Public Class Polygon
    Inherits GeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Rings As New List(Of CoordinateList)

    Public Overrides ReadOnly Property Coordinates()
        Get
            Return Rings
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim poly As New Polygon()
        If Not Rings Is Nothing Then
            poly.Rings.AddRange(Rings.Select(Function(ring) ring.CloneList(xform)))
        End If

        If Not BoundingBox Is Nothing Then
            poly.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return poly
    End Function
End Class