Public MustInherit Class GeoJsonGeometry(Of T)
    Inherits GeoJsonElement(Of T)

    <JsonProperty(PropertyName:="coordinates")>
    Public MustOverride ReadOnly Property Coordinates() As Object

    <JsonProperty(PropertyName:="bbox", Order:=5, NullValueHandling:=NullValueHandling.Ignore)>
    Public Property BoundingBox As Double()

    Public MustOverride Sub CreateFromDbGeometry(inp As DbGeometryWrapper)

    Friend Shared Function FromDbGeometry(inp As DbGeometryWrapper) As GeoJsonGeometry(Of T)
        Dim obj As GeoJsonGeometry(Of T) = CTypeDynamic(Activator.CreateInstance(Of T)(), GetType(T))

        obj.BoundingBox = New Double(3) {
            inp.Geometry.Envelope.PointAt(1).YCoordinate,
            inp.Geometry.Envelope.PointAt(1).XCoordinate,
            inp.Geometry.Envelope.PointAt(3).YCoordinate,
            inp.Geometry.Envelope.PointAt(3).XCoordinate
        }

        obj.CreateFromDbGeometry(inp)
        Return obj
    End Function

End Class