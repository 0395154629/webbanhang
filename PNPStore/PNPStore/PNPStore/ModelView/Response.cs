using Newtonsoft.Json;

namespace PNPStore.ModelView
{
    public class Response
    {
        public class ResponseAPI<T>
        {
            public string Message { get; set; }
            public int? Count { get; set; }

            public bool _isIgnoreNullData;
            private object _data;
            public object Data
            {
                get; set;
            }

            private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            public ResponseAPI(bool isIgnoreNullData = true)
            {
                _isIgnoreNullData = isIgnoreNullData;
            }
        }
    }
}
