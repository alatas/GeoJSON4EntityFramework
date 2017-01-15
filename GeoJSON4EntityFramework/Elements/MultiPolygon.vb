#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class MultiPolygon
    Inherits GeoJsonGeometry

    <JsonIgnore()>
    Public Property Polygons As New List(Of Polygon)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            Dim result As New List(Of Object)()
            For Each poly In Polygons
                result.Add(poly.Coordinates)
            Next
            Return result
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim mpl As New MultiPolygon()
        If Not Polygons Is Nothing Then
            mpl.Polygons.AddRange(Polygons.Select(Function(poly) CType(poly.Transform(xform), Polygon)))
        End If

        If Not BoundingBox Is Nothing Then
            mpl.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return mpl
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Polygons.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> GeometryType.Polygon Then Throw New ArgumentException
            Polygons.Add(FromDbGeometry(element))
        Next
    End Sub
End Class