Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports alatas.GeoJSON4EntityFramework
Imports System.Data.Entity.Spatial

<TestClass()>
Public Class Tests
    Private fc As FeatureCollection
    Private ReadOnly Property GetFeatureCollection() As FeatureCollection
        Get
            If fc Is Nothing Then
                fc = New FeatureCollection
                TestFeatures.ForEach(Sub(c)
                                         Dim geom As Entity.Spatial.DbGeometry = Entity.Spatial.DbGeometry.FromText(c.Geometry)
                                         Dim f = Feature.FromDbGeometry(geom)
                                         f.ID = c.ID
                                         f.Properties.Add("Name", c.Name)
                                         f.Properties.Add("Area", geom.Area)
                                         f.Properties.Add("Type", c.ElementType.ToString)
                                         fc.Features.Add(f)
                                     End Sub)
                Return fc
            End If

            Return fc
        End Get
    End Property

    <TestMethod()> Public Sub TestAll()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection, True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    <TestMethod()> Public Sub TestAllOnline()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection, False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub
End Class