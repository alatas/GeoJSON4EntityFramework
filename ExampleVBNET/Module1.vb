Imports alatas.GeoJSON4EntityFramework

Module Module1

    Private Menu As MenuItem() = {
        New MenuItem("WKT -> FeatureCollection", AddressOf FeatureCollectionFromWKT),
        New MenuItem("WKT -> Feature", AddressOf FeatureFromWKT),
        New MenuItem("WKT -> Geometry", AddressOf GeometryFromWKT),
        New MenuItem("Database -> FeatureCollection", AddressOf FeatureCollectionFromDB),
        New MenuItem("Database -> Feature", AddressOf FeatureFromDB),
        New MenuItem("Database -> Geometry", AddressOf GeometryFromDB)
    }

    Sub Main()

        Do
            Console.Clear()
            Console.WriteLine("GeoJson For EntityFramework Example")
            Console.WriteLine(StrDup(24, "-"))
            Console.WriteLine("Examples:")
            For i As Byte = 1 To Menu.Length
                Console.WriteLine(i & ". " & Menu(i - 1).Title)
            Next
            Console.Write("Enter the number (Q for Quit): ")
            Dim selection As String = Console.ReadLine
            If selection.ToUpper = "Q" Then Exit Do
            If IsNumeric(selection) AndAlso (selection >= 1 And selection <= Menu.Length) Then

                Console.WriteLine(StrDup(24, "-"))
                Dim outjson = Menu(selection - 1).Method.Invoke

                If outjson IsNot Nothing Then
                    Dim fileName As String = IO.Path.Combine(StartupPath.FullName, "\out" & Now.ToString("yyyyMMddHHmmss") & ".json")

                    IO.File.WriteAllText(fileName, outjson, Text.Encoding.UTF8)
                    Console.WriteLine("GeoJSON saved : " & fileName)
                End If

                Console.Read()
            End If
        Loop

    End Sub

    Function FeatureCollectionFromWKT() As String
        Dim WKTs = {"POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))",
                    "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))",
                    "LINESTRING (1 1, 2 2)"}

        Dim features = New FeatureCollection(WKTs)
        Return features.Serialize(prettyPrint:=True)
    End Function

    Function FeatureFromWKT() As String
        Dim WKT = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))"

        Dim feature = New Feature(WKT)
        Return feature.Serialize(prettyPrint:=True)
    End Function

    Function GeometryFromWKT() As String
        Dim WKT = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))"

        Dim geometry = GeoJsonGeometry.FromWKTGeometry(WKT)
        Return geometry.Serialize(prettyPrint:=True)
    End Function

    Function FeatureCollectionFromDB() As String
        If Not TestDBConnection() Then Return Nothing

        Using db As New SpatialExampleEntities
            Dim data = From row In db.SampleTables Select row.SpatialData

            Dim features = New FeatureCollection(data.ToArray)
            Return features.Serialize(prettyPrint:=True)
        End Using
    End Function

    Function FeatureFromDB() As String
        If Not TestDBConnection() Then Return Nothing

        Using db As New SpatialExampleEntities
            Dim data = From row In db.SampleTables Take 1 Select row.SpatialData

            Dim feature = New Feature(data.FirstOrDefault)
            Return feature.Serialize(prettyPrint:=True)
        End Using
    End Function

    Function GeometryFromDB() As String
        If Not TestDBConnection() Then Return Nothing

        Using db As New SpatialExampleEntities
            Dim data = From row In db.SampleTables Take 1 Select row.SpatialData

            Dim geometry = GeoJsonGeometry.FromDbGeometry(data.FirstOrDefault)
            Return geometry.Serialize(prettyPrint:=True)
        End Using
    End Function

#Region "Util"
    ReadOnly Property StartupPath As IO.DirectoryInfo
        Get
            Return New IO.DirectoryInfo(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location))
        End Get
    End Property

    Function TestDBConnection() As Boolean
        Console.WriteLine("Checking LocalDB Installation")
        Dim localDB As String = GetLocalDB()

        If localDB Is Nothing Then
            Console.WriteLine("LocalDB isn't installed, please download and install SQL Server LocalDB 2016+ from https://go.microsoft.com/fwlink/?LinkID=799012")
            Process.Start("https://go.microsoft.com/fwlink/?LinkID=799012")
            Return False
        Else

            Console.WriteLine("Locating sampla database file")
            Dim mdfPath As String = StartupPath.Parent.Parent.Parent.FullName & "\TestDB\SpatialExample.mdf"

            If Not IO.File.Exists(mdfPath) Then
                Console.WriteLine("Sample database file not found: " & mdfPath)
                Return False
            Else
                AppDomain.CurrentDomain.SetData("DataDirectory", StartupPath.Parent.Parent.Parent.FullName & "\TestDB\")

                Console.WriteLine("Connecting to MSSQLLocalDB instance")

                Dim c As New SqlClient.SqlConnection("data source=(LocalDB)\MSSQLLocalDB;integrated security=True;attachdbfilename=" & mdfPath & ";")

                Try
                    c.Open()
                    c.Close()
                Catch ex As Exception
                    Console.WriteLine("Error when connecting LocalDB instance: " & ex.Message)
                    Return False
                End Try

                Return True
            End If
        End If
    End Function

    Private Function GetLocalDB() As String
        Dim exeFileName As String = "SqlLocalD.exe"

        If IO.File.Exists(exeFileName) Then
            Return IO.Path.GetFullPath(exeFileName)
        End If

        For Each p As String In Environment.GetEnvironmentVariable("PATH").Split(";")
            Dim fullPath = IO.Path.Combine(p, exeFileName)
            If IO.File.Exists(fullPath) Then
                Return fullPath
            End If
        Next
        Return Nothing
    End Function
    Private Structure MenuItem
        Sub New(Title As String, Method As Func(Of String))
            Me.Title = Title
            Me.Method = Method
        End Sub
        Property Title As String
        Property Method As Func(Of String)
    End Structure

#End Region
End Module
