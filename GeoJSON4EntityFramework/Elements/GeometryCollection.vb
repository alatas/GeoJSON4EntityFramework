Partial Public Class GeometryCollection
    Inherits GeoJsonElement(Of GeometryCollection)
    Implements IGeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of IGeoJsonGeometry)

    Shared Function FromDbGeometry(inp As Entity.Spatial.DbGeometry) As IGeoJsonGeometry
        Dim obj As New GeometryCollection()
        obj.CreateFromDbGeometry(inp)
        Return obj
    End Function

End Class
