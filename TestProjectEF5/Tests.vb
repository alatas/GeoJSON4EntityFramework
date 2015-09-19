Imports alatas.GeoJSON4EntityFramework5

<TestClass()>
Public Class Tests
    Function GetFeatureCollection(Optional elementType As String = "", Optional withBBox As Boolean = False) As FeatureCollection
        Dim fc As New FeatureCollection
        TestFeatures.ForEach(Sub(c)
                                 If elementType = "" Or (elementType <> "" And elementType = c.ElementType.ToString) Then
                                     Dim geom = Spatial.DbGeometry.FromText(c.Geometry)
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

    <TestMethod()> Public Sub EF5TestAll()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(withBBox:=True), True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    <TestMethod()> Public Sub EF5OnlineTestAll()
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(withBBox:=True), False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub

    Public Sub TestSpecificType(elementType As String)
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(elementType.ToUpperInvariant), True)
        Assert.IsNotNull(json)
        WriteOutput(json)
    End Sub

    Public Sub TestSpecificTypeOnline(elementType As String)
        Dim json = GeoJsonSerializer.Serialize(Of FeatureCollection)(GetFeatureCollection(elementType.ToUpperInvariant), False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub
    <TestMethod> Sub EF5TestMultiPolygon()
        TestSpecificType("MultiPolygon")
    End Sub

    <TestMethod> Sub EF5TestPolygon()
        TestSpecificType("Polygon")
    End Sub

    <TestMethod> Sub EF5TestPoint()
        TestSpecificType("Point")
    End Sub

    <TestMethod> Sub EF5TestMultiPoint()
        TestSpecificType("MultiPoint")
    End Sub

    <TestMethod> Sub EF5TestLineString()
        TestSpecificType("LineString")
    End Sub

    <TestMethod> Sub EF5TestMultiLineString()
        TestSpecificType("MultiLineString")
    End Sub

    <TestMethod> Sub EF5TestGeometryCollection()
        TestSpecificType("GeometryCollection")
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiPolygon()
        TestSpecificTypeOnline("MultiPolygon")
    End Sub

    <TestMethod> Sub EF5OnlineTestPolygon()
        TestSpecificTypeOnline("Polygon")
    End Sub

    <TestMethod> Sub EF5OnlineTestPoint()
        TestSpecificTypeOnline("Point")
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiPoint()
        TestSpecificTypeOnline("MultiPoint")
    End Sub

    <TestMethod> Sub EF5OnlineTestLineString()
        TestSpecificTypeOnline("LineString")
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiLineString()
        TestSpecificTypeOnline("MultiLineString")
    End Sub

    <TestMethod> Sub EF5OnlineTestGeometryCollection()
        TestSpecificTypeOnline("GeometryCollection")
    End Sub
End Class