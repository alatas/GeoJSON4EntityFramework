Partial MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    Public MustOverride Sub CreateFromDbGeometry(inp As System.Data.Entity.Spatial.DbGeometry)

    Public Shared Function FromDbGeometry(inp As System.Data.Entity.Spatial.DbGeometry) As GeoJsonGeometry(Of T)
        Dim obj As GeoJsonGeometry(Of T) = CTypeDynamic(Activator.CreateInstance(Of T)(), GetType(T))

        obj.BoundingBox = New Double(3) {
            inp.Envelope.PointAt(1).YCoordinate,
            inp.Envelope.PointAt(1).XCoordinate,
            inp.Envelope.PointAt(3).YCoordinate,
            inp.Envelope.PointAt(3).XCoordinate
        }

        obj.CreateFromDbGeometry(inp)
        Return obj
    End Function
End Class