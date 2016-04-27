Public Interface IGeoJsonGeometry

    ''' <summary>
    ''' Gets the type of the geometry. This can be any constant under <see cref="GeometryType"/>
    ''' </summary>
    ''' <returns>The type of the geometry</returns>
    ReadOnly Property TypeName As String

End Interface