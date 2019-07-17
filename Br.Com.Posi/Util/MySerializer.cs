using System.IO;
using System.Xml.Serialization;

namespace Br.Com.Posi.Util
{
    public static class MySerializer<T>
    {
        public static string Serialize(T t)
        {
            XmlSerializer writer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {
                writer.Serialize(stringWriter, t);
                return stringWriter.ToString();
            }
        }

        public static T Deserialize(string xml)
        {
            XmlSerializer reader = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            {
                T t = (T)reader.Deserialize(stringReader);
                return t;
            }
        }
    }
}
