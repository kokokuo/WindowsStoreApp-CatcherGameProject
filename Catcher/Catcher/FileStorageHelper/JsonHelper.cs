using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Catcher.FileStorageHelper
{
    public class JsonHelper
    {
        public static string Serialize<T>(T data) where T : class 
        {
            DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream();
            jsonData.WriteObject(stream, data);
            byte[] bytes = stream.ToArray();
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static T Deserialize<T>(string jsonstring) where T : class
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonstring ));
            DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(T));
            return (T)jsonData.ReadObject(stream);
        }
    }
}
