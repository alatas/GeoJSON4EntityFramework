Public Class Polygon
    Inherits GeoJsonGeometry(Of Polygon)
    Implements IGeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Rings As New List(Of CoordinateList)

    Public Overrides ReadOnly Property Coordinates()
        Get
            Return Rings
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_BoundingBox As Double() Implements IGeoJsonGeometry.BoundingBox
        Get
            Return Me.BoundingBox
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim poly As New Polygon()

        If Not Me.Rings Is Nothing Then
            poly.Rings.AddRange(Me.Rings.Select(Function(ring) ring.CloneList(xform)))
        End If
        If Not Me.BoundingBox Is Nothing Then
            poly.BoundingBox = Coordinate.TransformBoundingBox(Me.BoundingBox, xform)
        End If

        Return poly
    End Function
End Class