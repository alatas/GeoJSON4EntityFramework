Partial MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)


    Public Shared Function FromDbGeometry(inp As System.Data.Spatial.DbGeometry) As GeoJsonGeometry(Of T)
        Return FromDbGeometry(New DbGeometryWrapper(inp))
    End Function
End Class