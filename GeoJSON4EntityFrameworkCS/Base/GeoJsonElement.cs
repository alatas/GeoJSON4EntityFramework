using Newtonsoft.Json;

namespace GeoJSON4EntityFramework.Base
{
    public class GeoJsonElement<T>
    {
        [JsonProperty(PropertyName = "type", Order = 1)]
        public string TypeName
        {
            get { return typeof (T).Name; }
        }
    }
}
