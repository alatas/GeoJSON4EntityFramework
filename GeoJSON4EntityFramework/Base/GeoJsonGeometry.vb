#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Imports System.Reflection

Public Class GeoJsonGeometry
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="coordinates")>
    Public Overridable ReadOnly Property Coordinates() As Object

    Public Overridable Sub CreateFromDbGeometry(inp As DbGeometry)
        Throw New NotImplementedException()
    End Sub

    Public Overridable Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        Throw New NotImplementedException()
    End Function

    <JsonProperty(PropertyName:="bbox", Order:=5, NullValueHandling:=NullValueHandling.Ignore)>
    Public Property BoundingBox As Double()

    <JsonIgnore>
    Public Property WithBoundingBox As Boolean = False

    Public Shared Function FromDbGeometry(inp As DbGeometry, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry
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

    Public Shared Function FromDbGeography(inp As DbGeography, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry
        Return FromDbGeometry(DbSpatialServices.Default.GeometryFromBinary(inp.AsBinary, inp.CoordinateSystemId), withBoundingBox)
    End Function

    Public Shared Function FromWKTGeometry(WKT As String, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry
        Return FromDbGeometry(DbGeometry.FromText(WKT), withBoundingBox)
    End Function

    Public Shared Function FromWKTGeography(WKT As String, Optional withBoundingBox As Boolean = True) As GeoJsonGeometry
        Dim instance = DbGeography.FromText(WKT)
        Return FromDbGeometry(DbSpatialServices.Default.GeometryFromBinary(instance.AsBinary, instance.CoordinateSystemId), withBoundingBox)
    End Function
End Class