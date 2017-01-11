Imports System.Data.Spatial
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
    <TestMethod> Sub EF5TestMultiPolygon()
        TestSpecificType(GeometryType.MultiPolygon)
    End Sub

    <TestMethod> Sub EF5TestPolygon()
        TestSpecificType(GeometryType.Polygon)
    End Sub

    <TestMethod> Sub EF5TestPoint()
        TestSpecificType(GeometryType.Point)
    End Sub

    <TestMethod> Sub EF5TestMultiPoint()
        TestSpecificType(GeometryType.MultiPoint)
    End Sub

    <TestMethod> Sub EF5TestLineString()
        TestSpecificType(GeometryType.LineString)
    End Sub

    <TestMethod> Sub EF5TestMultiLineString()
        TestSpecificType(GeometryType.MultiLineString)
    End Sub

    <TestMethod> Sub EF5TestGeometryCollection()
        TestSpecificType(GeometryType.GeometryCollection)
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiPolygon()
        TestSpecificTypeOnline(GeometryType.MultiPolygon)
    End Sub

    <TestMethod> Sub EF5OnlineTestPolygon()
        TestSpecificTypeOnline(GeometryType.Polygon)
    End Sub

    <TestMethod> Sub EF5OnlineTestPoint()
        TestSpecificTypeOnline(GeometryType.Point)
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiPoint()
        TestSpecificTypeOnline(GeometryType.MultiPoint)
    End Sub

    <TestMethod> Sub EF5OnlineTestLineString()
        TestSpecificTypeOnline(GeometryType.LineString)
    End Sub

    <TestMethod> Sub EF5OnlineTestMultiLineString()
        TestSpecificTypeOnline(GeometryType.MultiLineString)
    End Sub

    <TestMethod> Sub EF5OnlineTestGeometryCollection()
        TestSpecificTypeOnline(GeometryType.GeometryCollection)
    End Sub

    <TestMethod> Sub EF5TestGeometryCollectionFromDbGeometry()
        Dim wkt = "GEOMETRYCOLLECTION(POINT (30 10), LINESTRING (30 10, 10 30, 40 40), " &
                  "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10)), MULTIPOINT ((10 40), (40 30), (20 20), (30 10)), " &
                  "MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10)), " &
                  "MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5))))"

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

    <TestMethod> Sub EF5TestPointGeometryTransform()
        Dim geom = DbGeometry.FromText("POINT (1 1)", 4326)
        Dim pt As IGeoJsonGeometry = Point.FromDbGeometry(geom)
        Dim tx_pt = pt.Transform(AddressOf CoordDisplacer)
        Dim tx_pt2 As Point = CType(tx_pt, Point)
        Assert.AreEqual(Of Int32)(2, tx_pt2.Point.X)
        Assert.AreEqual(Of Int32)(2, tx_pt2.Point.Y)
    End Sub

    <TestMethod> Sub EF5TestMultiPointGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTIPOINT ((1 1), (2 2))", 4326)
        Dim mpt As IGeoJsonGeometry = MultiPoint.FromDbGeometry(geom)
        Dim tx_mpt = mpt.Transform(AddressOf CoordDisplacer)
        Dim tx_mpt2 As MultiPoint = CType(tx_mpt, MultiPoint)
        Assert.AreEqual(Of Int32)(2, tx_mpt2.Points(0).Point.X)
        Assert.AreEqual(Of Int32)(2, tx_mpt2.Points(0).Point.Y)
        Assert.AreEqual(Of Int32)(3, tx_mpt2.Points(1).Point.X)
        Assert.AreEqual(Of Int32)(3, tx_mpt2.Points(1).Point.Y)
    End Sub

    <TestMethod> Sub EF5TestLineStringGeometryTransform()
        Dim geom = DbGeometry.FromText("LINESTRING (1 1, 2 2)", 4326)
        Dim ls As IGeoJsonGeometry = LineString.FromDbGeometry(geom)
        Dim tx_ls = ls.Transform(AddressOf CoordDisplacer)
        Dim tx_ls2 As LineString = CType(tx_ls, LineString)
        Assert.AreEqual(Of Int32)(2, tx_ls2.Points(0).X)
        Assert.AreEqual(Of Int32)(2, tx_ls2.Points(0).Y)
        Assert.AreEqual(Of Int32)(3, tx_ls2.Points(1).X)
        Assert.AreEqual(Of Int32)(3, tx_ls2.Points(1).Y)
    End Sub

    <TestMethod> Sub EF5TestMultiLineStringGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTILINESTRING ((1 1, 2 2), (3 3, 4 4))", 4326)
        Dim mls As IGeoJsonGeometry = MultiLineString.FromDbGeometry(geom)
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

    <TestMethod> Sub EF5TestPolygonGeometryTransform()
        Dim geom = DbGeometry.FromText("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))", 4326)
        Dim ply As IGeoJsonGeometry = Polygon.FromDbGeometry(geom)
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

    <TestMethod> Sub EF5TestMultiPolygonGeometryTransform()
        Dim geom = DbGeometry.FromText("MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))", 4326)
        Dim ply As IGeoJsonGeometry = MultiPolygon.FromDbGeometry(geom)
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

    <TestMethod> Sub EF5TestGeometryCollectionTransform()
        Dim wkt = "GEOMETRYCOLLECTION(POINT (30 10), LINESTRING (30 10, 10 30, 40 40), " &
                  "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10)), MULTIPOINT ((10 40), (40 30), (20 20), (30 10)), " &
                  "MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10)), " &
                  "MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5))))"

        Dim geom = DbGeometry.FromText(wkt, 4326)
        Dim gc As IGeoJsonGeometry = GeometryCollection.FromDbGeometry(geom)
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