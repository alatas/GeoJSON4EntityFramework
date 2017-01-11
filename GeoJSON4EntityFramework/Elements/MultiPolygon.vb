Imports alatas.GeoJSON4EntityFramework

Public Class MultiPolygon
    Inherits GeoJsonGeometry(Of MultiPolygon)
    Implements IGeoJsonGeometry

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

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Private ReadOnly Property IGeoJsonGeometry_BoundingBox As Double() Implements IGeoJsonGeometry.BoundingBox
        Get
            Return Me.BoundingBox
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        Dim mpl As New MultiPolygon()
        If Not Me.Polygons Is Nothing Then
            mpl.Polygons.AddRange(Me.Polygons.Select(Function(poly) CType(poly.Transform(xform), Polygon)))
        End If
        If Not Me.BoundingBox Is Nothing Then
            mpl.BoundingBox = Coordinate.TransformBoundingBox(Me.BoundingBox, xform)
        End If
        Return mpl
    End Function
End Class