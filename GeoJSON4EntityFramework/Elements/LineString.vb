﻿Public Class LineString
    Inherits GeoJsonGeometry(Of LineString)
    Implements IGeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
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

    Private ReadOnly Property IGeoJsonGeometry_TypeName As String Implements IGeoJsonGeometry.TypeName
        Get
            Return Me.TypeName
        End Get
    End Property

    Public Function Transform(xform As CoordinateTransform) As IGeoJsonGeometry Implements IGeoJsonGeometry.Transform
        If xform Is Nothing Then
            Throw New ArgumentNullException(NameOf(xform))
        End If

        Dim line As New LineString()
        If Not Me.Points Is Nothing Then
            line.Points = Me.Points.CloneList(xform)
        End If
        Return line
    End Function

End Class