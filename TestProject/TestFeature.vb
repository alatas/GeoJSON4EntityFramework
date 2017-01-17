Public Class TestFeature
    Sub New(_name As String, _geometry As String, _file As String)
        MyBase.New()

        Name = _name
        Geometry = _geometry
        File = _file
        ElementType = Text.RegularExpressions.Regex.Match(Geometry, "^([A-Z]*)\s").Value.Trim
    End Sub

    Property ID As String = Guid.NewGuid.ToString.ToLower
    Property Name As String
    Property File As String
    Property Geometry As String
    Property ElementType As String
End Class
