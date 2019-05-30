using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Sys.Tool
{
    [Serializable()]
    public abstract class Serialization
    {
        public  void SerializeObjectToFile(object obj, string FileName)
        {
            if (!obj.GetType().IsSerializable)
            {
                throw new Exception("Object cannot be serialized.");
            }

            string data = "";
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                data = Convert.ToBase64String(stream.ToArray());
            }             
            System.IO.File.WriteAllText(FileName, data);

        }
        public  object DeserializeObjectFromFile(string FileName)
        {
            try
            {
                string data = System.IO.File.ReadAllText(FileName);
                byte[] bytes = Convert.FromBase64String(data);

                object obj;
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    obj = new BinaryFormatter().Deserialize(stream);
                }
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
    }
}