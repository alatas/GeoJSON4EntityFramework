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
                            '--Warning BC4232--
                            'Using the iteration variable In a lambda expression may have unexpected results.  
                            'Instead, create a local variable within the Loop And assign it the value Of the iteration variable.
                            For i As Integer = 0 To Points3.Count
                                out(k)(i) = Points3(i).Coordinate
                            Next
                            '--
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