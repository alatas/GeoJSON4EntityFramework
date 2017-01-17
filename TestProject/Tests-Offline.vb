
#If EF5 Then
Imports System.Data.Spatial
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
Imports alatas.GeoJSON4EntityFramework
#End If

Partial Public Class TestsBase

    <TestMethod, TestCategory("Offline_File")> Sub TestRoads()
        TestSpecificFile("roads.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_File")> Sub TestInnerPoints()
        TestSpecificFile("inner_points.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_File")> Sub TestInnerRoads()
        TestSpecificFile("inner_roads.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_File")> Sub TestMausoleumArea()
        TestSpecificFile("mausoleum_area.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_File")> Sub TestPoints()
        TestSpecificFile("points.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_File")> Sub TestResidentialArea()
        TestSpecificFile("residential_area.wkt")
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestMultiPolygon()
        TestSpecificType(GeometryType.MultiPolygon)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestPolygon()
        TestSpecificType(GeometryType.Polygon)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestPoint()
        TestSpecificType(GeometryType.Point)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestMultiPoint()
        TestSpecificType(GeometryType.MultiPoint)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestLineString()
        TestSpecificType(GeometryType.LineString)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestMultiLineString()
        TestSpecificType(GeometryType.MultiLineString)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestGeometryCollection()
        TestSpecificType(GeometryType.GeometryCollection)
    End Sub

    <TestMethod, TestCategory("Offline_Type")> Sub TestGeometryCollection2()
        Dim wkt = "GEOMETRYCOLLECTION(POINT (30 10), LINESTRING (30 10, 10 30, 40 40), 
                  POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10)), MULTIPOINT ((10 40), (40 30), (20 20), (30 10)), 
                  MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10)),
                  MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5))))"

        Dim geom = DbGeometry.FromText(wkt, 4326)
        Dim gc As GeometryCollection = GeometryCollection.FromDbGeometry(geom)

        Assert.AreEqual(6, gc.Geometries.Count)
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.Point).Count())
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.MultiPoint).Count())
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.LineString).Count())
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.MultiLineString).Count())
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.Polygon).Count())
        Assert.AreEqual(1, gc.Geometries.Where(Function(g) g.TypeName = GeometryType.MultiPolygon).Count())
    End Sub

End Class
