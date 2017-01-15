Public Class TestFeature
    Sub New(_name As String, _geometry As String)
        MyBase.New()

        Name = _name
        Geometry = _geometry
        ElementType = Text.RegularExpressions.Regex.Match(Geometry, "^([A-Z]*)\s").Value
    End Sub

    Property ID As String = Guid.NewGuid.ToString.ToLower
    Property Name As String
    Property Geometry As String
    Property ElementType As String
End Class
