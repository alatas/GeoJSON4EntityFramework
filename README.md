#GeoJSON4EntityFramework ![Logo](https://raw.githubusercontent.com/alatas/GeoJSON4EntityFramework/master/geojson.png) 
___

###What is GeoJSON
[GeoJSON](http://geojson.org/) is a format for encoding a variety of geographic data structures. A GeoJSON object may represent a geometry, a feature, or a collection of features. GeoJSON supports the following geometry types: Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon, and GeometryCollection. Features in GeoJSON contain a geometry object and additional properties, and a feature collection represents a list of features.<sup>[*](http://geojson.org/geojson-spec.html#introduction)</sup>

###What is GeoJSON4EntityFramework
GeoJSON4EntityFramework allows you to create GeoJSON output from Entity Framework Spatial Data. 

###Features
- [x] Supports Entity Framework v6 (System.Data.Entity.Spatial namespace) and Entity Framework v5 (System.Data.Spatial namespace) objects
- [x] Supports DbGeometry (*planar*) and DbGeography (*geodetic "round earth"*) objects
- [x] Supports all types of features defined in geojson [specs](http://geojson.org/geojson-spec.html)
- [x] Supports boundingbox property defined in geojson [specs](http://geojson.org/geojson-spec.html)

###Install
#####Install with Package Manager Console - Nuget
To install GeoJSON for Entity Framework, run the following command in the Package Manager Console

Entity Framework 6
> `Install-Package GeoJSON4EntityFramework`

Entity Framework 5
> `Install-Package GeoJSON4EntityFramework5`

#####Manual Install
Download the latest [release](https://github.com/alatas/GeoJSON4EntityFramework/releases) and add to your project references manually

###Prerequisites
* Microsoft® System CLR Types for Microsoft® SQL Server® (x86/x64) [(SQLSysClrTypes.msi)](http://www.microsoft.com/en-us/download/details.aspx?id=35580)

###Tests and Validation
You may validate outputs with http://geojson.io and http://geojsonlint.com
