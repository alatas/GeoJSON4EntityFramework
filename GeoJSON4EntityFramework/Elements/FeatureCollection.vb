#If EF5 Then
Imports System.Data.Spatial
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
#End If

Public Class FeatureCollection
    Inherits GeoJsonElement

    <JsonProperty(PropertyName:="features")>
    Public Property Features As New List(Of Feature)

    Sub New()
        MyBase.New
    End Sub

    Sub New(feature As Feature)
        MyBase.New
        Features.Add(feature)
    End Sub

    Sub New(features() As Feature)
        MyBase.New
        Me.Features.AddRange(features)
    End Sub

    Sub New(geom As GeoJsonGeometry)
        MyBase.New
        Features.Add(New Feature(geom))
    End Sub

    Sub New(geoms() As GeoJsonGeometry)
        MyBase.New
        Features.AddRange(Array.ConvertAll(geoms, New Converter(Of GeoJsonGeometry, Feature)(Function(geom) (New Feature(geom)))))
    End Sub

    Sub New(geom As DbGeometry, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.Add(New Feature(geom, withBoundingBox))
    End Sub

    Sub New(geoms() As DbGeometry, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.AddRange(Array.ConvertAll(geoms, New Converter(Of DbGeometry, Feature)(Function(geom) (New Feature(geom, withBoundingBox)))))
    End Sub

    Sub New(geog As DbGeography, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.Add(New Feature(geog, withBoundingBox))
    End Sub

    Sub New(geogs() As DbGeography, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.AddRange(Array.ConvertAll(geogs, New Converter(Of DbGeography, Feature)(Function(geom) (New Feature(geom, withBoundingBox)))))
    End Sub

    Sub New(WKT As String, Optional WKTasGeography As Boolean = False, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.Add(New Feature(WKT, WKTasGeography, withBoundingBox))
    End Sub

    Sub New(WKTs() As String, Optional WKTasGeography As Boolean = False, Optional withBoundingBox As Boolean = True)
        MyBase.New
        Features.AddRange(Array.ConvertAll(WKTs, New Converter(Of String, Feature)(Function(geom) (New Feature(geom, WKTasGeography, withBoundingBox)))))
    End Sub
End Class