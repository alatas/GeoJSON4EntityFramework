
#If EF5 Then
Imports System.Data.Spatial
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
Imports alatas.GeoJSON4EntityFramework
#End If


Partial Class TestsBase
    Public Function GetSpecificTypeCollection(elementType As String, Optional withBBox As Boolean = False) As FeatureCollection
        Dim fc As New FeatureCollection
        TestFeatures.ForEach(Sub(c)
                                 If elementType.ToUpperInvariant = c.ElementType Then
                                     Dim f As New Feature(c.Geometry, withBBox)
                                     f.ID = c.ID
                                     f.Properties.Add("Name", c.Name)
                                     f.Properties.Add("File", c.File)
                                     f.Properties.Add("Type", c.ElementType)
                                     fc.Features.Add(f)
                                 End If
                             End Sub)
        Return fc
    End Function

    Public Function GetSpecificFileCollection(file As String, Optional withBBox As Boolean = False) As FeatureCollection
        Dim fc As New FeatureCollection
        TestFeatures.ForEach(Sub(c)
                                 If file = c.File Then
                                     Dim f As New Feature(c.Geometry, withBBox)
                                     f.ID = c.ID
                                     f.Properties.Add("Name", c.Name)
                                     f.Properties.Add("File", c.File)
                                     f.Properties.Add("Type", c.ElementType)
                                     fc.Features.Add(f)
                                 End If
                             End Sub)
        Return fc
    End Function

    Public Sub TestSpecificType(elementType As String)
        Dim fc = GetSpecificTypeCollection(elementType.ToUpperInvariant)
        Assert.AreNotEqual(0, fc.Features.Count)

        For Each f In fc.Features
            Assert.AreEqual(elementType.ToUpperInvariant(), f.Geometry.TypeName.ToUpperInvariant())
        Next

        Dim json = GeoJsonSerializer.Serialize(fc, True)
        Assert.IsNotNull(json)
        Assert.AreNotEqual(json, "{""type"":""FeatureCollection"",""features"":[]}")
        WriteOutput(json)
    End Sub

    Public Sub TestSpecificTypeOnline(elementType As String)
        Dim fc = GetSpecificTypeCollection(elementType.ToUpperInvariant)
        Assert.AreNotEqual(0, fc.Features.Count)

        For Each f In fc.Features
            Assert.AreEqual(elementType.ToUpperInvariant(), f.Geometry.TypeName.ToUpperInvariant())
        Next

        Dim json = GeoJsonSerializer.Serialize(fc, False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        TestOutputOnline(json)
    End Sub


    Public Sub TestSpecificFile(file As String)
        Dim fc = GetSpecificFileCollection(file)
        Assert.AreNotEqual(0, fc.Features.Count)

        For Each f In fc.Features
            Assert.AreEqual(file, f.Properties("File").ToString)
        Next

        Dim json = GeoJsonSerializer.Serialize(fc, True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    Public Sub TestSpecificFileOnline(file As String)
        Dim fc = GetSpecificFileCollection(file)
        Assert.AreNotEqual(0, fc.Features.Count)

        For Each f In fc.Features
            Assert.AreEqual(file, f.Properties("File").ToString)
        Next
        Dim json = GeoJsonSerializer.Serialize(fc, False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        TestOutputOnline(json)
    End Sub
End Class