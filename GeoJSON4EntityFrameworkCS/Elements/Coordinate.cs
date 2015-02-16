namespace GeoJSON4EntityFramework.Elements
{
    public class Coordinate
    {
        public Coordinate()
        {
        }

        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double[] Coordinates
        {
            get { return new [] { X, Y }; }
        }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
