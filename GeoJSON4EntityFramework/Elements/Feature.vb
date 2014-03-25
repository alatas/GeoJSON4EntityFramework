Public Class Feature
    Inherits GeoJsonElement(Of Feature)
    <JsonProperty(PropertyName:="id", Order:=2)>
    Public Property ID As String

    <JsonProperty(PropertyName:="properties", Order:=3)>
    Public Property Properties As New Dictionary(Of String, String)

    <JsonProperty(PropertyName:="geometry", Order:=4)>
    <JsonConverter(GetType(GenericListConverter(Of IGeoJsonGeometry)))>
    Public Property Geometry As New List(Of IGeoJsonGeometry)

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ParamArray Geometries() As IGeoJsonGeometry)
        MyBase.New()
        Geometry = Geometries.ToList
    End Sub

    Public Shared Function FromDbGeometry(inp As Entity.Spatial.DbGeometry) As Feature
        Dim f As New Feature

        Select Case inp.SpatialTypeName
            Case "MultiPolygon"
                f.Geometry.Add(MultiPolygon.FromDbGeometry(inp))
            Case "Polygon"
                f.Geometry.Add(Polygon.FromDbGeometry(inp))
            Case "Point"
                f.Geometry.Add(Point.FromDbGeometry(inp))
            Case "MultPoint"
                f.Geometry.Add(MultiPoint.FromDbGeometry(inp))
            Case Else
                Throw New NotImplementedException
        End Select

        Return f
    End Function
End Class