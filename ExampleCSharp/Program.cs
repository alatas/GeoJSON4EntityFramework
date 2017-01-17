using alatas.GeoJSON4EntityFramework;
using ExampleCSharp;
using System;
using System.Data;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;


static class Module1
{

    private static MenuItem[] Menu = {
        new MenuItem("WKT -> FeatureCollection", FeatureCollectionFromWKT),
        new MenuItem("WKT -> Feature", FeatureFromWKT),
        new MenuItem("WKT -> Geometry", GeometryFromWKT),
        new MenuItem("Database -> FeatureCollection", FeatureCollectionFromDB),
        new MenuItem("Database -> Feature", FeatureFromDB),
        new MenuItem("Database -> Geometry", GeometryFromDB)

    };

    public static void Main()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("GeoJson For EntityFramework Example");
            Console.WriteLine(new String('-', 24));
            Console.WriteLine("Examples:");

            for (byte i = 1; i <= Menu.Length; i++)
            {
                Console.WriteLine(i + ". " + Menu[i - 1].Title);
            }

            Console.Write("Enter the number (Q for Quit): ");

            string selection = Console.ReadLine();

            if (selection.ToUpper() == "Q")
                break;

            int intSelection;
            if (Int32.TryParse(selection, out intSelection))
            {

                if (intSelection >= 1 & intSelection <= Menu.Length)
                {

                    Console.WriteLine(new String('-', 24));
                    dynamic outjson = Menu[intSelection - 1].Method.Invoke();

                    if (outjson != null)
                    {
                        string fileName = Path.Combine(StartupPath.FullName, "out" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json");

                        File.WriteAllText(fileName, outjson, System.Text.Encoding.UTF8);
                        Console.WriteLine("GeoJSON saved : " + fileName);
                    }

                    Console.Read();
                }
            }

        } while (true);

    }

    public static string FeatureCollectionFromWKT()
    {
        string[] WKTs = {
            "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))",
            "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))",
            "LINESTRING (1 1, 2 2)"
        };

        FeatureCollection features = new FeatureCollection(WKTs);
        return features.Serialize(prettyPrint: true);
    }

    public static string FeatureFromWKT()
    {
        string WKT = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))";

        Feature feature = new Feature(WKT);
        return feature.Serialize(prettyPrint: true);
    }

    public static string GeometryFromWKT()
    {
        string WKT = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))";

        GeoJsonGeometry geometry = GeoJsonGeometry.FromWKTGeometry(WKT);
        return geometry.Serialize(prettyPrint: true);
    }

    public static string FeatureCollectionFromDB()
    {
        if (!TestDBConnection())
            return null;

        using (Entities db = new Entities())
        {

            DbGeometry[] data = (from row in db.SampleTables select row.SpatialData).ToArray();

            FeatureCollection features = new FeatureCollection(data);
            return features.Serialize(prettyPrint: true);
        }
    }

    public static string FeatureFromDB()
    {
        if (!TestDBConnection())
            return null;

        using (Entities db = new Entities())
        {
            DbGeometry data = (from row in db.SampleTables select row.SpatialData).FirstOrDefault();

            Feature feature = new Feature(data);
            return feature.Serialize(prettyPrint: true);
        }
    }

    public static string GeometryFromDB()
    {
        if (!TestDBConnection())
            return null;


        using (Entities db = new Entities())
        {
            DbGeometry data = (from row in db.SampleTables select row.SpatialData).FirstOrDefault();

            GeoJsonGeometry geometry = GeoJsonGeometry.FromDbGeometry(data);
            return geometry.Serialize(prettyPrint: true);
        }
    }

    #region "Util"
    public static DirectoryInfo StartupPath
    {
        get
        {
            return new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
    }

    public static bool TestDBConnection()
    {
        Console.WriteLine("Checking LocalDB Installation");
        string localDB = GetLocalDB();

        if (localDB == null)
        {
            Console.WriteLine("LocalDB isn't installed, please download and install SQL Server LocalDB 2016+ from https://go.microsoft.com/fwlink/?LinkID=799012");
            Process.Start("https://go.microsoft.com/fwlink/?LinkID=799012");
            return false;

        }
        else
        {
            Console.WriteLine("Locating sampla database file");
            string mdfPath = StartupPath.Parent.Parent.Parent.FullName + "\\TestDB\\SpatialExample.mdf";

            if (!File.Exists(mdfPath))
            {
                Console.WriteLine("Sample database file not found: " + mdfPath);
                return false;
            }
            else
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", StartupPath.Parent.Parent.Parent.FullName + "\\TestDB\\");

                Console.WriteLine("Connecting to MSSQLLocalDB instance");

                SqlConnection c = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB;integrated security=True;attachdbfilename=" + mdfPath + ";");

                try
                {
                    c.Open();
                    c.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when connecting LocalDB instance: " + ex.Message);
                    return false;
                }

                return true;
            }
        }
    }

    private static string GetLocalDB()
    {
        string exeFileName = "SqlLocalDB.exe";

        if (File.Exists(exeFileName))
        {
            return Path.GetFullPath(exeFileName);
        }

        foreach (string p in Environment.GetEnvironmentVariable("PATH").Split(';'))
        {
            dynamic fullPath = Path.Combine(p, exeFileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }
        return null;
    }
    private struct MenuItem
    {
        public MenuItem(string Title, Func<string> Method)
        {
            this.Title = Title;
            this.Method = Method;
        }
        public string Title { get; set; }
        public Func<string> Method { get; set; }
    }

    #endregion
}