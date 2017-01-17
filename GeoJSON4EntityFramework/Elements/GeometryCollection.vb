#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class GeometryCollection
    Inherits GeoJsonGeometry

    <JsonProperty(PropertyName:="geometries")>
    Public Property Geometries As New List(Of GeoJsonGeometry)

    <JsonIgnore>
    Public Overrides ReadOnly Property Coordinates()

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim gc As New GeometryCollection()
        If Not Geometries Is Nothing Then
            gc.Geometries.AddRange(Geometries.Select(Function(g) g.Transform(xform)))
        End If

        If Not BoundingBox Is Nothing Then
            gc.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return gc
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Geometries.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            Geometries.Add(FromDbGeometry(element, WithBoundingBox))
        Next
    End Sub
End Class
