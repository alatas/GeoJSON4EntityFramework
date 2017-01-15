
#If EF5 Then
Imports alatas.GeoJSON4EntityFramework5
#End If

#If EF6 Then
Imports alatas.GeoJSON4EntityFramework
#End If


Partial Public MustInherit Class TestsBase
    Public TestFeatures As New List(Of TestFeature)


    <TestInitialize()>
    Sub Init()
        Console.Out.WriteLine("Test Init")
        Console.Out.WriteLine("You may need to install SQLCLRTypes first for spatial data types ")
        Console.Out.WriteLine("Please download SQLSysClrTypes.msi from https://www.microsoft.com/en-us/download/details.aspx?id=49999")
        Console.Out.WriteLine("For x64 OS, you have to install both x86 and x64")
        Console.Out.WriteLine(StrDup(25, "-"))

        Dim i As Integer = 1
        Dim testWKTFolder As New IO.DirectoryInfo("../../../TestWKTs/")

        For Each wktFile In testWKTFolder.GetFiles("*.wkt")
            Using sRead As New IO.StreamReader(wktFile.FullName)
                Dim geom As String = ""
                Do
                    Dim buffer = sRead.ReadLine
                    If Not (buffer = "" Or sRead.EndOfStream) Then
                        geom &= buffer
                    Else
                        TestFeatures.Add(New TestFeature("Feature" & i, geom & buffer))
                        geom = ""
                        i += 1
                        If sRead.EndOfStream Then Exit Do
                    End If
                Loop
            End Using
        Next

        Console.Out.WriteLine($"Total {i} test feature added.")
        Console.Out.WriteLine("Test Init End")
    End Sub


    Public MustOverride Sub TestSpecificType(elementType As String)

    Public MustOverride Sub TestSpecificTypeOnline(elementType As String)

    Public MustOverride Function GetFeatureCollection(Optional elementType As String = "", Optional withBBox As Boolean = False) As FeatureCollection

    Public Property TestContext() As TestContext

    Public Sub WriteOutput(json As String)
        Dim fileName As String = TestContext.DeploymentDirectory & "\" & TestContext.TestName & "_out.json"
        IO.File.WriteAllText(fileName, json)
        TestContext.AddResultFile(fileName)
        Console.Out.WriteLine("Output saved in " & fileName)
    End Sub

    Public Sub SendOutput(json As String)
        Console.Out.WriteLine("sending output to geojsonlint.com")

        Dim buffer() As Byte = Text.Encoding.UTF8.GetBytes(json)
        Dim webReq As Net.HttpWebRequest = Net.WebRequest.Create("http://geojsonlint.com/validate")

        webReq.Method = "POST"
        webReq.ContentLength = buffer.Length
        webReq.ContentType = "application/x-www-form-urlencoded"

        Dim reqStream = webReq.GetRequestStream()
        reqStream.Write(buffer, 0, buffer.Length)
        reqStream.Close()

        Dim webRes = webReq.GetResponse
        Dim resStream = webRes.GetResponseStream
        Dim resReader As New IO.StreamReader(resStream)
        Dim resObj = Newtonsoft.Json.JsonConvert.DeserializeObject(Of GeoJSONLintResult)(resReader.ReadToEnd)
        Console.Out.WriteLine("result: " & resObj.status)
        Console.Out.WriteLine("message: " & resObj.message)

        Assert.AreEqual(resObj.status, "ok")
    End Sub

    Private Class GeoJSONLintResult
        Public Property status As String
        Public Property message As String
    End Class
End Class
