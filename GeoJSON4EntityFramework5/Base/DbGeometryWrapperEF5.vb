Public Class DbGeometryWrapper
    Public Property Geometry As System.Data.Spatial.DbGeometry

    Public Sub New(GeometryParam As System.Data.Spatial.DbGeometry)
        Geometry = GeometryParam
    End Sub

End Class
