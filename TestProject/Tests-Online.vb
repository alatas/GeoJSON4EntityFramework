
#If EF5 Then
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports alatas.GeoJSON4EntityFramework
#End If

Partial Public Class TestsBase

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestRoads()
        TestSpecificFileOnline("roads.wkt")
    End Sub

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestInnerPoints()
        TestSpecificFileOnline("inner_points.wkt")
    End Sub

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestInnerRoads()
        TestSpecificFileOnline("inner_roads.wkt")
    End Sub

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestMausoleumArea()
        TestSpecificFileOnline("mausoleum_area.wkt")
    End Sub

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestPoints()
        TestSpecificFileOnline("points.wkt")
    End Sub

    <TestMethod, TestCategory("Online_File")> Sub OnlineTestResidentialArea()
        TestSpecificFileOnline("residential_area.wkt")
    End Sub



    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestMultiPolygon()
        TestSpecificTypeOnline(GeometryType.MultiPolygon)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestPolygon()
        TestSpecificTypeOnline(GeometryType.Polygon)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestPoint()
        TestSpecificTypeOnline(GeometryType.Point)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestMultiPoint()
        TestSpecificTypeOnline(GeometryType.MultiPoint)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestLineString()
        TestSpecificTypeOnline(GeometryType.LineString)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestMultiLineString()
        TestSpecificTypeOnline(GeometryType.MultiLineString)
    End Sub

    <TestMethod, TestCategory("Online_Type")> Sub OnlineTestGeometryCollection()
        TestSpecificTypeOnline(GeometryType.GeometryCollection)
    End Sub

End Class
