Public Class Polygon
    Inherits GeoJsonGeometry(Of Polygon)
    Implements IGeoJsonGeometry

    <Newtonsoft.Json.JsonIgnore()>
    Public Property PointsList As New List(Of CoordinateList)

    Public Overrides ReadOnly Property Coordinates()
        Get
            Try
                If PointsList.Count = 0 Then
                    Return New Double() {}
                Else
                    Dim out(PointsList.Count - 1)()() As Double
                    For k As Integer = 0 To PointsList.Count - 1
                        Dim Points3 As CoordinateList = PointsList(k)
                        If Points3.Count = 0 Then
                            Return New Double() {}
                        ElseIf Points3.Count = 1 Then
                            Throw New Exception("There must be an array of two or more points")
                        Else
                            out(k) = New Double(Points3.Count - 1)() {}
                            Parallel.For(0, Points3.Count, Sub(i)
                                                               out(k)(i) = Points3(i).Coordinate
                                                           End Sub)
                        End If
                    Next
                    Return out
                End If
            Catch ex As Exception
                Return New Double() {}
            End Try
        End Get
    End Property

End Class