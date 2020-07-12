using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RateEngine.Core.Helper
{
    public static class JsonHelper
    {
        public static T Deserialise<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string Serialise<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
