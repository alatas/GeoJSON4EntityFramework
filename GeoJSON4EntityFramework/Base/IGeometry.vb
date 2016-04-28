''' <summary>
''' A function that transforms the given coordinate
''' </summary>
''' <param name="x"></param>
''' <param name="y"></param>
''' <param name="tx"></param>
''' <param name="ty"></param>
''' <returns></returns>
Public Delegate Function CoordinateTransform(ByVal x As Double, ByVal y As Double, ByRef tx As Double, ByRef ty As Double) As Boolean

Public Interface IGeoJsonGeometry

    ''' <summary>
    ''' Gets the type of the geometry. This can be any constant under <see cref="GeometryType"/>
    ''' </summary>
    ''' <returns>The type of the geometry</returns>
    ReadOnly Property TypeName As String

    ''' <summary>
    ''' Returns a transformed instance of the given geometry using the suppled coordinate transform function. If it has
    ''' a bounding box specified, it too will also be transformed
    ''' </summary>
    ''' <param name="xform">The coordinate transform function</param>
    ''' <returns></returns>
    Function Transform(ByVal xform As CoordinateTransform) As IGeoJsonGeometry

    ''' <summary>
    ''' Returns the bounding box of this geometry instance. This is null if the geometry was created from a call to
    ''' FromDbGeometry or FromDbGeography with withBoundingBox = false
    ''' 
    ''' If set, the bounding box follows the format: [minY, minX, maxY, maxX]
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property BoundingBox As Double()

End Interface