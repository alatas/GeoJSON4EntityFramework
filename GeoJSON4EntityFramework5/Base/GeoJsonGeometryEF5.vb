Partial MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    Public MustOverride Sub CreateFromDbGeometry(inp As Spatial.DbGeometry)

    Public Shared Function FromDbGeometry(inp As Spatial.DbGeometry, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry(Of T)
        Dim obj As GeoJsonGeometry(Of T) = CTypeDynamic(Activator.CreateInstance(Of T)(), GetType(T))

        If withBoundingBox Then
            obj.WithBoundingBox = True
            obj.BoundingBox = New Double() {
            inp.Envelope.PointAt(1).YCoordinate,
            inp.Envelope.PointAt(1).XCoordinate,
            inp.Envelope.PointAt(3).YCoordinate,
            inp.Envelope.PointAt(3).XCoordinate
        }

        End If

        obj.CreateFromDbGeometry(inp)
        Return obj
    End Function
End Class