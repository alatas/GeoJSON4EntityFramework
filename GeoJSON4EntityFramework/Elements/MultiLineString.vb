Public Class MultiLineString
    Inherits GeoJsonGeometry(Of MultiLineString)
    Implements IGeoJsonGeometry

    <JsonIgnore()>
    Public Property LineStrings As New List(Of LineString)

    Public Overrides ReadOnly Property Coordinates As Object
        Get
            'TODO: Complete
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides Sub CreateFromDbGeometry(inp As DbGeometryWrapper)
        If inp.Geometry.SpatialTypeName <> MyBase.TypeName Then Throw New ArgumentException
        LineStrings.Clear()

        For i As Integer = 1 To inp.Geometry.ElementCount
            Dim element = inp.Geometry.ElementAt(i)
            If element.SpatialTypeName <> "LineString" Then Throw New ArgumentException
            LineStrings.Add(LineString.FromDbGeometry(New DbGeometryWrapper(element)))
        Next
    End Sub

End Class
