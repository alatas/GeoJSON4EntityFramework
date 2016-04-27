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
    ''' Returns a transformed instance of the given geometry using the suppled coordinate transform function
    ''' </summary>
    ''' <param name="xform">The coordinate transform function</param>
    ''' <returns></returns>
    Function Transform(ByVal xform As CoordinateTransform) As IGeoJsonGeometry

End Interface