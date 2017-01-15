#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class MultiPoint
    Inherits GeoJsonGeometry

    <JsonIgnore>
    Public Property Points As New List(Of Point)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            If Points.Count = 0 Then
                Return New Double() {}
            Else
                Dim out(Points.Count - 1)() As Double

                Parallel.For(0, Points.Count, Sub(i)
                                                  out(i) = Points(i).Coordinates
                                              End Sub)
                Return out
            End If
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim mpt As New MultiPoint()
        If Not Points Is Nothing Then
            mpt.Points.AddRange(Points.Select(Function(pt) CType(pt.Transform(xform), Point)))
        End If

        If Not BoundingBox Is Nothing Then
            mpt.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return mpt
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.ElementCount
            Dim element = inp.ElementAt(i)
            If element.SpatialTypeName <> GeometryType.Point Then Throw New ArgumentException
            Points.Add(FromDbGeometry(element))
        Next
    End Sub
End Class