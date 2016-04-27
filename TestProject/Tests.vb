Imports alatas.GeoJSON4EntityFramework

<TestClass()>
Public Class Tests

    Function GetFeatureCollection(Optional elementType As String = "", Optional withBBox As Boolean = False) As FeatureCollection
        Dim fc As New FeatureCollection
        TestFeatures.ForEach(Sub(c)
                                 If elementType = "" Or (elementType <> "" And elementType = c.ElementType.ToString) Then
                                     Dim geom = Entity.Spatial.DbGeometry.FromText(c.Geometry)
                                     Dim f = Feature.FromDbGeometry(geom, withBBox)
                                     f.ID = c.ID
                                     f.Properties.Add("Name", c.Name)
                                     f.Properties.Add("Area", geom.Area)
                                     f.Properties.Add("Type", c.ElementType.ToString)
                                     fc.Features.Add(f)
                                 End If
                             End Sub)
        Return fc
    End Function

    <TestMethod()> Public Sub TestAll()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(withBBox:=True), True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    <TestMethod()> Public Sub OnlineTestAll()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(withBBox:=True), False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub

    Public Sub TestSpecificType(elementType As String)
        Dim fc = GetFeatureCollection(elementType.ToUpperInvariant)
        For Each f In fc.Features
            For Each g In f.Geometry
                Assert.AreEqual(elementType.ToUpperInvariant(), g.TypeName.ToUpperInvariant())
            Next
        Next
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(fc, True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    Public Sub TestSpecificTypeOnline(elementType As String)
        Dim fc = GetFeatureCollection(elementType.ToUpperInvariant)
        For Each f In fc.Features
            For Each g In f.Geometry
                Assert.AreEqual(elementType.ToUpperInvariant(), g.TypeName.ToUpperInvariant())
            Next
        Next
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(fc, False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub
    <TestMethod> Sub TestMultiPolygon()
        TestSpecificType("MultiPolygon")
    End Sub

    <TestMethod> Sub TestPolygon()
        TestSpecificType("Polygon")
    End Sub

    <TestMethod> Sub TestPoint()
        TestSpecificType("Point")
    End Sub

    <TestMethod> Sub TestMultiPoint()
        TestSpecificType("MultiPoint")
    End Sub

    <TestMethod> Sub TestLineString()
        TestSpecificType("LineString")
    End Sub

    <TestMethod> Sub TestMultiLineString()
        TestSpecificType("MultiLineString")
    End Sub

    <TestMethod> Sub TestGeometryCollection()
        TestSpecificType("GeometryCollection")
    End Sub

    <TestMethod> Sub OnlineTestMultiPolygon()
        TestSpecificTypeOnline("MultiPolygon")
    End Sub

    <TestMethod> Sub OnlineTestPolygon()
        TestSpecificTypeOnline("Polygon")
    End Sub

    <TestMethod> Sub OnlineTestPoint()
        TestSpecificTypeOnline("Point")
    End Sub

    <TestMethod> Sub OnlineTestMultiPoint()
        TestSpecificTypeOnline("MultiPoint")
    End Sub

    <TestMethod> Sub OnlineTestLineString()
        TestSpecificTypeOnline("LineString")
    End Sub

    <TestMethod> Sub OnlineTestMultiLineString()
        TestSpecificTypeOnline("MultiLineString")
    End Sub

    <TestMethod> Sub OnlineTestGeometryCollection()
        TestSpecificTypeOnline("GeometryCollection")
    End Sub

End Class