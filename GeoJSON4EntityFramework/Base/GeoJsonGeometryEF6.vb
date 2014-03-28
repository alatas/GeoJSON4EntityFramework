Partial MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    Public MustOverride Sub CreateFromDbGeometry(inp As System.Data.Entity.Spatial.DbGeometry)

    Public Shared Function FromDbGeometry(inp As System.Data.Entity.Spatial.DbGeometry) As GeoJsonGeometry(Of T)
        Dim obj As GeoJsonGeometry(Of T) = CTypeDynamic(Activator.CreateInstance(Of T)(), GetType(T))
        obj.CreateFromDbGeometry(inp)
        Return obj
    End Function
End Class