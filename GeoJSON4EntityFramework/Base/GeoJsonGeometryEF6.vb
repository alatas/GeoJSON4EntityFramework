Imports System.Reflection

Partial MustInherit Class GeoJsonGeometry
    Inherits GeoJsonElement

    Public MustOverride Sub CreateFromDbGeometry(inp As Entity.Spatial.DbGeometry)

    Public Shared Function FromDbGeometry(inp As Entity.Spatial.DbGeometry, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry

        Dim baseType As Type = GetType(GeoJsonGeometry)
        Dim geomType = (From t In Assembly.GetAssembly(baseType).GetTypes()
                        Where t.IsSubclassOf(baseType) And t.Name = inp.SpatialTypeName Select t).FirstOrDefault

        If geomType Is Nothing Then
            Throw New NotImplementedException($"Geometry/Geography type not handled: {inp.SpatialTypeName}")
        Else
            Dim obj As GeoJsonGeometry = CTypeDynamic(Activator.CreateInstance(geomType), geomType)

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
        End If
    End Function
End Class