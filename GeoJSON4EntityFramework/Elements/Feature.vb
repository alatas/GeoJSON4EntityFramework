#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If
Public Class Feature
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="id", Order:=2, NullValueHandling:=NullValueHandling.Ignore)>
    Public Property ID As String = Nothing

    <JsonProperty(PropertyName:="properties", Order:=3)>
    Public Property Properties As New Dictionary(Of String, Object)

    <JsonProperty(PropertyName:="geometry", Order:=4)>
    Public Property Geometry As GeoJsonGeometry

    Sub New()
        MyBase.New()
    End Sub

    Sub New(geom As GeoJsonGeometry)
        MyBase.New
        Geometry = geom
    End Sub

    Sub New(geom As DbGeometry, Optional withBoundingBox As Boolean = False)
        Geometry = GeoJsonGeometry.FromDbGeometry(geom, withBoundingBox)
    End Sub

    Sub New(geog As DbGeography, Optional withBoundingBox As Boolean = False)
        Geometry = GeoJsonGeometry.FromDbGeography(geog, withBoundingBox)
    End Sub

    Sub New(wkt As String, Optional WKTasGeography As Boolean = False, Optional withBoundingBox As Boolean = False)
        If WKTasGeography Then
            Geometry = GeoJsonGeometry.FromWKTGeography(wkt, withBoundingBox)
        Else
            Geometry = GeoJsonGeometry.FromWKTGeometry(wkt, withBoundingBox)
        End If
    End Sub

    Public Shared Function FromDbGeometry(inp As DbGeometry, Optional withBoundingBox As Boolean = False) As Feature
        Return New Feature(GeoJsonGeometry.FromDbGeometry(inp, withBoundingBox))
    End Function

    Public Shared Function FromDbGeography(inp As DbGeography, Optional withBoundingBox As Boolean = False) As Feature
        Return New Feature(GeoJsonGeometry.FromDbGeography(inp, withBoundingBox))
    End Function

    Public Shared Function FromWKTGeometry(WKT As String, Optional withBoundingBox As Boolean = False) As Feature
        Return New Feature(GeoJsonGeometry.FromWKTGeometry(WKT, withBoundingBox))
    End Function

    Public Shared Function FromWKTGeography(WKT As String, Optional withBoundingBox As Boolean = False) As Feature
        Return New Feature(GeoJsonGeometry.FromWKTGeography(WKT, withBoundingBox))
    End Function
End Class