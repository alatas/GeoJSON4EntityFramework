Public Class OrderedContractResolver
    Inherits Newtonsoft.Json.Serialization.DefaultContractResolver

    Protected Overrides Function CreateProperties(type As Type, memberSerialization As Newtonsoft.Json.MemberSerialization) As IList(Of Newtonsoft.Json.Serialization.JsonProperty)
        Return (From p In MyBase.CreateProperties(type, memberSerialization) Let order As Byte = IIf(p.Order Is Nothing, 99, p.Order) Order By order, p.PropertyName Select p).ToList
    End Function
End Class
