
#If EF5 Then
Imports System.Data.Spatial
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports System.Data.Entity.Spatial
Imports alatas.GeoJSON4EntityFramework
#End If

Partial Public Class TestsBase

    ''' <summary>
    ''' A simple transformation function that displaces all coordinate values by +1
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <param name="tx"></param>
    ''' <param name="ty"></param>
    ''' <returns></returns>
    Function CoordDisplacer(ByVal x As Double, y As Double, ByRef tx As Double, ByRef ty As Double) As Boolean
        tx = Math.Floor(x + 1)
        ty = Math.Floor(y + 1)
        Return True
    End Function

    <TestMethod, TestCategory("Transform")> Sub TestPointGeometryTransform()
        Dim geom = DbGeometry.FromText("POINT (1 1)", 4326)
        Dim pt As GeoJsonGeometry = Point.FromDbGeometry(geom)
        Dim tx_pt = pt.Transform(AddressOf CoordDisplacer)
        Dim tx_pt2 As Point = CType(tx_pt, Point)
        Assert.AreEqual(Of Int32)(2, tx_pt2.Point.X)
        Assert.AreEqual(Of Int32)(2, tx_pt2.Point.Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestMultiPointGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTIPOINT ((1 1), (2 2))", 4326)
        Dim mpt As GeoJsonGeometry = MultiPoint.FromDbGeometry(geom)
        Dim tx_mpt = mpt.Transform(AddressOf CoordDisplacer)
        Dim tx_mpt2 As MultiPoint = CType(tx_mpt, MultiPoint)
        Assert.AreEqual(Of Int32)(2, tx_mpt2.Points(0).Point.X)
        Assert.AreEqual(Of Int32)(2, tx_mpt2.Points(0).Point.Y)
        Assert.AreEqual(Of Int32)(3, tx_mpt2.Points(1).Point.X)
        Assert.AreEqual(Of Int32)(3, tx_mpt2.Points(1).Point.Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestLineStringGeometryTransform()
        Dim geom = DbGeometry.FromText("LINESTRING (1 1, 2 2)", 4326)
        Dim ls As GeoJsonGeometry = LineString.FromDbGeometry(geom)
        Dim tx_ls = ls.Transform(AddressOf CoordDisplacer)
        Dim tx_ls2 As LineString = CType(tx_ls, LineString)
        Assert.AreEqual(Of Int32)(2, tx_ls2.Points(0).X)
        Assert.AreEqual(Of Int32)(2, tx_ls2.Points(0).Y)
        Assert.AreEqual(Of Int32)(3, tx_ls2.Points(1).X)
        Assert.AreEqual(Of Int32)(3, tx_ls2.Points(1).Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestMultiLineStringGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTILINESTRING ((1 1, 2 2), (3 3, 4 4))", 4326)
        Dim mls As GeoJsonGeometry = MultiLineString.FromDbGeometry(geom)
        Dim tx_mls = mls.Transform(AddressOf CoordDisplacer)
        Dim tx_mls2 As MultiLineString = CType(tx_mls, MultiLineString)
        Assert.AreEqual(Of Int32)(2, tx_mls2.LineStrings(0).Points(0).X)
        Assert.AreEqual(Of Int32)(2, tx_mls2.LineStrings(0).Points(0).Y)
        Assert.AreEqual(Of Int32)(3, tx_mls2.LineStrings(0).Points(1).X)
        Assert.AreEqual(Of Int32)(3, tx_mls2.LineStrings(0).Points(1).Y)
        Assert.AreEqual(Of Int32)(4, tx_mls2.LineStrings(1).Points(0).X)
        Assert.AreEqual(Of Int32)(4, tx_mls2.LineStrings(1).Points(0).Y)
        Assert.AreEqual(Of Int32)(5, tx_mls2.LineStrings(1).Points(1).X)
        Assert.AreEqual(Of Int32)(5, tx_mls2.LineStrings(1).Points(1).Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestPolygonGeometryTransform()
        Dim geom = DbGeometry.FromText("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))", 4326)
        Dim ply As GeoJsonGeometry = Polygon.FromDbGeometry(geom)
        Dim tx_ply = ply.Transform(AddressOf CoordDisplacer)
        Dim tx_ply2 As Polygon = CType(tx_ply, Polygon)
        Assert.AreEqual(Of Int32)(31, tx_ply2.Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(21, tx_ply2.Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(21, tx_ply2.Rings(0)(3).Y)
        Assert.AreEqual(Of Int32)(31, tx_ply2.Rings(0)(4).X)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Rings(0)(4).Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestMultiPolygonGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))", 4326)
        Dim ply As GeoJsonGeometry = MultiPolygon.FromDbGeometry(geom)
        Dim tx_ply = ply.Transform(AddressOf CoordDisplacer)
        Dim tx_ply2 As MultiPolygon = CType(tx_ply, MultiPolygon)
        Assert.AreEqual(Of Int32)(31, tx_ply2.Polygons(0).Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(21, tx_ply2.Polygons(0).Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(46, tx_ply2.Polygons(0).Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Polygons(0).Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Polygons(0).Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Polygons(0).Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(31, tx_ply2.Polygons(0).Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(21, tx_ply2.Polygons(0).Rings(0)(3).Y)


        Assert.AreEqual(Of Int32)(16, tx_ply2.Polygons(1).Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(6, tx_ply2.Polygons(1).Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(41, tx_ply2.Polygons(1).Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Polygons(1).Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Polygons(1).Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(21, tx_ply2.Polygons(1).Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(6, tx_ply2.Polygons(1).Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(11, tx_ply2.Polygons(1).Rings(0)(3).Y)
        Assert.AreEqual(Of Int32)(16, tx_ply2.Polygons(1).Rings(0)(4).X)
        Assert.AreEqual(Of Int32)(6, tx_ply2.Polygons(1).Rings(0)(4).Y)
    End Sub

    <TestMethod, TestCategory("Transform")> Sub TestGeometryCollectionTransform()
        Dim wkt = "GEOMETRYCOLLECTION(POINT (30 10), LINESTRING (30 10, 10 30, 40 40), 
                  POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10)), MULTIPOINT ((10 40), (40 30), (20 20), (30 10)),
                  MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10)), 
                  MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5))))"

        Dim geom = DbGeometry.FromText(wkt, 4326)
        Dim gc As GeoJsonGeometry = GeometryCollection.FromDbGeometry(geom)
        Dim tx_gc = gc.Transform(AddressOf CoordDisplacer)
        Dim tx_gc2 As GeometryCollection = CType(tx_gc, GeometryCollection)

        Dim pt = CType(tx_gc2.Geometries(0), Point)
        Dim ls = CType(tx_gc2.Geometries(1), LineString)
        Dim pl = CType(tx_gc2.Geometries(2), Polygon)
        Dim mpt = CType(tx_gc2.Geometries(3), MultiPoint)
        Dim mls = CType(tx_gc2.Geometries(4), MultiLineString)
        Dim mpl = CType(tx_gc2.Geometries(5), MultiPolygon)

        Assert.AreEqual(Of Int32)(31, pt.Point.X)
        Assert.AreEqual(Of Int32)(11, pt.Point.Y)

        Assert.AreEqual(Of Int32)(31, ls.Points(0).X)
        Assert.AreEqual(Of Int32)(11, ls.Points(0).Y)
        Assert.AreEqual(Of Int32)(11, ls.Points(1).X)
        Assert.AreEqual(Of Int32)(31, ls.Points(1).Y)
        Assert.AreEqual(Of Int32)(41, ls.Points(2).X)
        Assert.AreEqual(Of Int32)(41, ls.Points(2).Y)

        Assert.AreEqual(Of Int32)(31, pl.Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(11, pl.Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(41, pl.Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(41, pl.Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(21, pl.Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(41, pl.Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(11, pl.Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(21, pl.Rings(0)(3).Y)
        Assert.AreEqual(Of Int32)(31, pl.Rings(0)(4).X)
        Assert.AreEqual(Of Int32)(11, pl.Rings(0)(4).Y)

        Assert.AreEqual(Of Int32)(11, mpt.Points(0).Point.X)
        Assert.AreEqual(Of Int32)(41, mpt.Points(0).Point.Y)
        Assert.AreEqual(Of Int32)(41, mpt.Points(1).Point.X)
        Assert.AreEqual(Of Int32)(31, mpt.Points(1).Point.Y)
        Assert.AreEqual(Of Int32)(21, mpt.Points(2).Point.X)
        Assert.AreEqual(Of Int32)(21, mpt.Points(2).Point.Y)
        Assert.AreEqual(Of Int32)(31, mpt.Points(3).Point.X)
        Assert.AreEqual(Of Int32)(11, mpt.Points(3).Point.Y)

        Assert.AreEqual(Of Int32)(11, mls.LineStrings(0).Points(0).X)
        Assert.AreEqual(Of Int32)(11, mls.LineStrings(0).Points(0).Y)
        Assert.AreEqual(Of Int32)(21, mls.LineStrings(0).Points(1).X)
        Assert.AreEqual(Of Int32)(21, mls.LineStrings(0).Points(1).Y)
        Assert.AreEqual(Of Int32)(11, mls.LineStrings(0).Points(2).X)
        Assert.AreEqual(Of Int32)(41, mls.LineStrings(0).Points(2).Y)
        Assert.AreEqual(Of Int32)(41, mls.LineStrings(1).Points(0).X)
        Assert.AreEqual(Of Int32)(41, mls.LineStrings(1).Points(0).Y)
        Assert.AreEqual(Of Int32)(31, mls.LineStrings(1).Points(1).X)
        Assert.AreEqual(Of Int32)(31, mls.LineStrings(1).Points(1).Y)
        Assert.AreEqual(Of Int32)(41, mls.LineStrings(1).Points(2).X)
        Assert.AreEqual(Of Int32)(21, mls.LineStrings(1).Points(2).Y)
        Assert.AreEqual(Of Int32)(31, mls.LineStrings(1).Points(3).X)
        Assert.AreEqual(Of Int32)(11, mls.LineStrings(1).Points(3).Y)

        Assert.AreEqual(Of Int32)(31, mpl.Polygons(0).Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(21, mpl.Polygons(0).Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(46, mpl.Polygons(0).Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(41, mpl.Polygons(0).Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(11, mpl.Polygons(0).Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(41, mpl.Polygons(0).Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(31, mpl.Polygons(0).Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(21, mpl.Polygons(0).Rings(0)(3).Y)
        Assert.AreEqual(Of Int32)(16, mpl.Polygons(1).Rings(0)(0).X)
        Assert.AreEqual(Of Int32)(6, mpl.Polygons(1).Rings(0)(0).Y)
        Assert.AreEqual(Of Int32)(41, mpl.Polygons(1).Rings(0)(1).X)
        Assert.AreEqual(Of Int32)(11, mpl.Polygons(1).Rings(0)(1).Y)
        Assert.AreEqual(Of Int32)(11, mpl.Polygons(1).Rings(0)(2).X)
        Assert.AreEqual(Of Int32)(21, mpl.Polygons(1).Rings(0)(2).Y)
        Assert.AreEqual(Of Int32)(6, mpl.Polygons(1).Rings(0)(3).X)
        Assert.AreEqual(Of Int32)(11, mpl.Polygons(1).Rings(0)(3).Y)
        Assert.AreEqual(Of Int32)(16, mpl.Polygons(1).Rings(0)(4).X)
        Assert.AreEqual(Of Int32)(6, mpl.Polygons(1).Rings(0)(4).Y)
    End Sub
End Class
