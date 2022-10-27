using Lab3.WebApi.Models.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Lab3.WebApi.Formatters
{
    public class ExpandoXmlMediaTypeFormatter : MediaTypeFormatter
    {
        public ExpandoXmlMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml"));
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml"));
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(ExpandoObject) || type == typeof(object) || type == typeof(LinkedResourceCollection<object>)
                || type == typeof(ICollection<object>) || type == typeof(ICollection<ExpandoObject>);
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(ExpandoObject) || type == typeof(object) || type == typeof(LinkedResourceCollection<object>)
                || type == typeof(ICollection<object>) || type == typeof(ICollection<ExpandoObject>);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var ser = new XmlSerializer(type);
                return ser.Deserialize(readStream);
            });
            return task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var xml = JsonConvert.DeserializeXNode("{\"root\":" +
                         JsonConvert.SerializeObject(value) + "}").ToString();
                using (var writer = XmlTextWriter.Create(writeStream,
                       new XmlWriterSettings() { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Response");
                    writer.WriteRaw(xml.Replace("<root>",
                       string.Empty).Replace("</root>", string.Empty));
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                writeStream.Flush();
            });
            return task;
        }
    }
}