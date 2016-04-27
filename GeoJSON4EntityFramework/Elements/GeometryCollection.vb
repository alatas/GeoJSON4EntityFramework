Imports alatas.GeoJSON4EntityFramework

Partial Public Class GeometryCollection
    Inherits GeoJsonGeometry(Of GeometryCollection)
    Implements IGeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of IGeoJsonGeometry)

    <JsonIgnore>
    Public Overrides ReadOnly Property Coordinates()

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        Dim gc As New GeometryCollection()
        If Not Me.Geometries Is Nothing Then
            gc.Geometries.AddRange(Me.Geometries.Select(Function(g) g.Transform(xform)))
        End If
        Return gc
    End Function
End Class
