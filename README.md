# GeoJSON4EntityFramework ![Logo](https://raw.githubusercontent.com/alatas/GeoJSON4EntityFramework/master/geojson.png) ![BuildStatus](https://sukru.visualstudio.com/_apis/public/build/definitions/ef35124c-d2ad-4375-9c78-8862c095207b/1/badge) 

___

### What is GeoJSON?
[GeoJSON](http://geojson.org/) is a format for encoding a variety of geographic data structures. A GeoJSON object may represent a geometry, a feature, or a collection of features. 

In 2015, the Internet Engineering Task Force (IETF), in conjunction with the original specification authors, formed a GeoJSON WG to standardize GeoJSON. [RFC 7946](https://tools.ietf.org/html/rfc7946) was published in August 2016 and is the new standard specification of the GeoJSON format, replacing the 2008 GeoJSON specification.

GeoJSON supports _Point_, _LineString_, _Polygon_, _MultiPoint_, _MultiLineString_, _MultiPolygon_, and _GeometryCollection_ geometry types. 

_Feature_ contain a geometry object and additional properties, and a _FeatureCollection_ represents a list of features. 

For example, A house, a road and a bus stop represents three different _Feature_. All of them might have different type of geometries. House could be a _polygon_, road could be a _linestring_ and bus stop could be a _point_. All of them represents a neighbourhood and this called _FeatureCollection_ in GeoJSON.

### What is EntityFramework?
EntityFramework (EF) is an open source object-relational mapping [(ORM)](https://en.wikipedia.org/wiki/Object-relational_mapping) framework for Microsoft .net. It allows us to use database rows as class instances.

### What is Well-known Text (WKT)?
[Well-known Text (WKT)](https://en.wikipedia.org/wiki/Well-known_text) is a text markup language for representing vector geometry objects on a map, spatial reference systems of spatial objects and transformations between spatial reference systems. In summary, It's a text representations of geometrical objects.

### So, What is GeoJSON4EntityFramework ??
_GeoJSON for EntityFramework_ is a .net library that allows you to create GeoJSON output from EntityFramework Spatial Data or WKT inputs. In other words, It serializes different type of geometry objects to GeoJSON. It's not limited to only EF entities but It can serialize WKT inputs as well.

### Features
- [x] Supports Entity Framework v6 (System.Data.Entity.Spatial namespace) and Entity Framework v5 (System.Data.Spatial namespace) objects
- [x] Supports Well-known Text inputs
- [x] Supports DbGeometry (*planar*) and DbGeography (*geodetic "round earth"*) objects
- [x] Supports all types of features defined in geojson specs ([RFC 7946](https://tools.ietf.org/html/rfc7946))
- [x] Supports boundingbox property defined in geojson specs ([RFC 7946](https://tools.ietf.org/html/rfc7946))
- [x] Supports geometry transform

___
### Quick Start
#### EntityFramework Example
**Visual Basic**
```vbnet
Imports alatas.GeoJSON4EntityFramework

Function GetGeoJSONFromDB() As String
    Using db As New SpatialExampleEntities
        Dim data = From row In db.SampleTables Select row.SpatialData

        Dim features as New FeatureCollection(data.ToArray)
        Return features.Serialize(prettyPrint:=True)
    End Using
End Function
```

**C#**
```csharp
using alatas.GeoJSON4EntityFramework;

public string GetGeoJSONFromDB()
{
    using (Entities db = new Entities())
    {

    DbGeometry[] data = (from row in db.SampleTables select row.SpatialData).ToArray();
    
    FeatureCollection features = new FeatureCollection(data);
    return features.Serialize(prettyPrint: true);
    }
}
```

#### Well-Known Text (WKT) Example
**Visual Basic**
```vbnet
Imports alatas.GeoJSON4EntityFramework

Function GetGeoJSONFromWKT() As String
    Dim WKTs = {"POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))",
                "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))",
                "LINESTRING (1 1, 2 2)"}

    Dim features as New FeatureCollection(WKTs)
    Return features.Serialize(prettyPrint:=True)
End Function
```

**C#**
```csharp
using alatas.GeoJSON4EntityFramework;

public string GetGeoJSONFromWKT()
{
    string[] WKTs = {
        "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))",
        "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))",
        "LINESTRING (1 1, 2 2)"
    };

    FeatureCollection features = new FeatureCollection(WKTs);
    return features.Serialize(prettyPrint: true);
}
```
---
### Install
#### Install with Package Manager Console - Nuget
To install GeoJSON for Entity Framework, run the following command in the Package Manager Console

**Entity Framework 6**
```powershell
Install-Package GeoJSON4EntityFramework
```

**Entity Framework 5**
```powershell
Install-Package GeoJSON4EntityFramework5
```



#### Manual Install
Download the latest [release](https://github.com/alatas/GeoJSON4EntityFramework/releases) and add to your project references manually

### Prerequisites
* Microsoft® System CLR Types for Microsoft® SQL Server® (x86/x64) [(SQLSysClrTypes.msi)](https://www.microsoft.com/en-us/download/details.aspx?id=49999)

### Tests and Validation
You may validate outputs with http://geojson.io and http://geojsonlint.com
___
Test data extracted from OpenStreetMap®. OpenStreetMap® is open data, licensed under the [Open Data Commons Open Database License](http://opendatacommons.org/licenses/odbl/) (ODbL) by the OpenStreetMap Foundation (OSMF)
