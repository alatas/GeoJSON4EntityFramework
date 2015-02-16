Public Class DbGeometryWrapper
    Public Property Geometry As System.Data.Entity.Spatial.DbGeometry

    Public Sub New(GeometryParam As System.Data.Entity.Spatial.DbGeometry)
        Geometry = GeometryParam
    End Sub

End Class
