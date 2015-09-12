Public Class TestFeature
    Sub New(_name As String, _geometry As String)
        MyBase.New()

        Name = _name
        Geometry = _geometry

        Dim ElementTypeName As String = Text.RegularExpressions.Regex.Match(Geometry, "^([A-Z]*)\s").Value
        If Not ElementTypeName = "" Then
            ElementType = [Enum].Parse(GetType(ElementTypeEnum), ElementTypeName)
        Else
            ElementType = ElementTypeEnum.UNKNOWN
        End If
    End Sub

    Property ID As String = Guid.NewGuid.ToString.ToLower
    Property Name As String
    Property Geometry As String
    Property ElementType As ElementTypeEnum

    Public Enum ElementTypeEnum
        UNKNOWN
        POINT
        MULTIPOINT
        LINESTRING
        MULTILINESTRING
        POLYGON
        MULTIPOLYGON
        GEOMETRYCOLLECTION
    End Enum
End Class
