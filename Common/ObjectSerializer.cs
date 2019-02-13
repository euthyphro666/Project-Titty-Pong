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

        public static byte[] SerializeState(this GameState state)
        {
            using (var s = new MemoryStream())
            {
                using (var buffer = new BinaryWriter(s))
                {
                    buffer.Write(state.LastProcessedInputNumber);
                    buffer.Write(state.Nipple.Body.X);
                    buffer.Write(state.Nipple.Body.Y);
                    buffer.Write(state.Nipple.Force.X);
                    buffer.Write(state.Nipple.Force.Y);
                    buffer.Write(state.ClientA.Body.X);
                    buffer.Write(state.ClientA.Body.X);
                    buffer.Write(state.ClientB.Body.X);
                    buffer.Write(state.ClientB.Body.Y);
                }

                return s.ToArray();
            }
        }
        
        public static GameState DeserializeState(this byte[] objBytes)
        {
            using (var s = new MemoryStream())
            {
                using (var buffer = new BinaryReader(s))
                {
                    var state = new GameState {LastProcessedInputNumber = buffer.ReadInt32()};
                    state.Nipple.Body.X = buffer.ReadInt32(); 
                    state.Nipple.Body.X = buffer.ReadInt32(); 
                    state.Nipple.Force.X = buffer.ReadInt32(); 
                    state.Nipple.Force.Y = buffer.ReadInt32(); 
                    state.ClientA.Body.X = buffer.ReadInt32(); 
                    state.ClientA.Body.Y = buffer.ReadInt32(); 
                    state.ClientB.Body.X = buffer.ReadInt32(); 
                    state.ClientB.Body.Y = buffer.ReadInt32();

                    return state;
                }
            }
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