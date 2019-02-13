using System.IO;
using System.Text;
using Common.Game_Data;
using Common.Messages;
using Newtonsoft.Json;

namespace Common
{
    public static class ObjectSerializer
    {
        public static byte[] Serialize(this object obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static string DeserializeToJsonString(this byte[] objBytes)
        {
            return Encoding.UTF8.GetString(objBytes);
        }

        public static T Deserialize<T>(this byte[] objBytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(objBytes));
        }
        
        public static T Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
}
}