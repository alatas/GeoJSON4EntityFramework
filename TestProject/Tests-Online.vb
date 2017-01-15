
#If EF5 Then
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports alatas.GeoJSON4EntityFramework
#End If

Partial Public MustInherit Class TestsBase

    <TestMethod()> Public Sub OnlineTestAll()
        Dim json = GeoJsonSerializer.Serialize(GetFeatureCollection(withBBox:=True), False)
        Assert.IsNotNull(json)
        WriteOutput(json)
        SendOutput(json)
    End Sub

    <TestMethod> Sub OnlineTestMultiPolygon()
        TestSpecificTypeOnline(GeometryType.MultiPolygon)
    End Sub

    <TestMethod> Sub OnlineTestPolygon()
        TestSpecificTypeOnline(GeometryType.Polygon)
    End Sub

    <TestMethod> Sub OnlineTestPoint()
        TestSpecificTypeOnline(GeometryType.Point)
    End Sub

    <TestMethod> Sub OnlineTestMultiPoint()
        TestSpecificTypeOnline(GeometryType.MultiPoint)
    End Sub

    <TestMethod> Sub OnlineTestLineString()
        TestSpecificTypeOnline(GeometryType.LineString)
    End Sub

    <TestMethod> Sub OnlineTestMultiLineString()
        TestSpecificTypeOnline(GeometryType.MultiLineString)
    End Sub

    <TestMethod> Sub OnlineTestGeometryCollection()
        TestSpecificTypeOnline(GeometryType.GeometryCollection)
    End Sub

End Class
