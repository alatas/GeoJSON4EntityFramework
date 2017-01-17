#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class LineString
    Inherits GeoJsonGeometry

    <JsonIgnore()>
    Public Property Points As New CoordinateList

    Public Overrides ReadOnly Property Coordinates()
        Get
            Try
                If Points.Count = 0 Then
                    Return New Double() {}
                ElseIf Points.Count = 1 Then
                    Throw New Exception("There must be an array of two or more points")
                Else
                    Dim out()() As Double
                    out = New Double(Points.Count - 1)() {}
                    Parallel.For(0, Points.Count, Sub(i)
                                                      out(i) = Points(i).Coordinate
                                                  End Sub)
                    Return out
                End If
            Catch ex As Exception
                Return New Double() {}
            End Try
        End Get
    End Property

    Public Overrides Function Transform(xform As CoordinateTransform) As GeoJsonGeometry
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim line As New LineString()
        If Not Points Is Nothing Then
            line.Points = Points.CloneList(xform)
        End If

        If Not BoundingBox Is Nothing Then
            line.BoundingBox = TransformFunctions.TransformBoundingBox(BoundingBox, xform)
        End If
        Return line
    End Function

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometry)
        If inp.SpatialTypeName <> TypeName Then Throw New ArgumentException
        Points.Clear()

        For i As Integer = 1 To inp.PointCount
            Dim point = inp.PointAt(i)
            Points.AddNew(point.XCoordinate, point.YCoordinate)
        Next
    End Sub
End Class
