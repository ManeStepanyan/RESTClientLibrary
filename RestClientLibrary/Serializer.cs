using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RestClientLibrary
{
    /// <summary>
    /// Deserializer and serializer to support XML and JSON format
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// Deserializing
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="input">JSON or XML string</param>
        /// <param name="contentType">Indicating type</param>
        /// <returns>List of model items</returns>
        public List<T> Deserialize<T>(string input, string contentType) where T : class
        {
            if (contentType == "application/xml")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (var str = new StringReader(input))
                {
                    return (List<T>)serializer.Deserialize(str);
                }
            }
            else if (contentType == "application/json")
                return JsonConvert.DeserializeObject<List<T>>(input);

            return default(List<T>);
        }
        /// <summary>
        /// Deserialize to only one object instead of list
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="input">String to deserialize</param>
        /// <param name="contentType">Content type</param>
        /// <returns>Model</returns>
        public T DeserializeToObject<T>(string input, string contentType) where T : class
        {
            if (contentType == "application/xml")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (var str = new StringReader(input))
                {
                    return (T)serializer.Deserialize(str);
                }
            }
            else if (contentType == "application/json")
                return JsonConvert.DeserializeObject<T>(input);

            return default(T);
        }

        /// <summary>
        /// Serializer
        /// </summary>
        /// <typeparam name="T">Type of my model</typeparam>
        /// <param name="ObjectToSerialize">Object to serialize</param>
        /// <param name="contentType">Type of content to serialize into</param>
        /// <returns></returns>
        public string Serialize<T>(T ObjectToSerialize, string contentType)
        {
            if (contentType == "application/xml")
            {
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                    return textWriter.ToString();
                }
            }
            else if (contentType == "application/json")
            {
                return JsonConvert.SerializeObject(ObjectToSerialize);
            }
            return "";
        }

    }
}
