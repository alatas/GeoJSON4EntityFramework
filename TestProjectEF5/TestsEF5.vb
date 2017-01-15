Imports alatas.GeoJSON4EntityFramework5

<TestClass()>
Public Class TestsEF5
    Inherits TestsBase

    Public Overrides Function GetFeatureCollection(Optional elementType As String = "", Optional withBBox As Boolean = False) As FeatureCollection
        Dim fc As New FeatureCollection
        TestFeatures.ForEach(Sub(c)
                                 If elementType = "" Or (elementType <> "" And elementType = c.ElementType) Then
                                     Dim geom = Spatial.DbGeometry.FromText(c.Geometry)
                                     Dim f = Feature.FromDbGeometry(geom, withBBox)
                                     f.ID = c.ID
                                     f.Properties.Add("Name", c.Name)
                                     f.Properties.Add("Area", geom.Area)
                                     f.Properties.Add("Type", c.ElementType)
                                     fc.Features.Add(f)
                                 End If
                             End Sub)
        Return fc
    End Function

    Public Overrides Sub TestSpecificType(elementType As String)
        Dim fc = GetFeatureCollection(elementType.ToUpperInvariant)
        For Each f In fc.Features
            Assert.AreEqual(elementType.ToUpperInvariant(), f.Geometry.TypeName.ToUpperInvariant())
        Next
        Dim json = GeoJsonSerializer.Serialize(fc, True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    Public Overrides Sub TestSpecificTypeOnline(elementType As String)
        Dim fc = GetFeatureCollection(elementType.ToUpperInvariant)
        For Each f In fc.Features
            Assert.AreEqual(elementType.ToUpperInvariant(), f.Geometry.TypeName.ToUpperInvariant())
        Next
        Dim json = GeoJsonSerializer.Serialize(fc, False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub
End Class