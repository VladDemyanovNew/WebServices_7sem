using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Xml;

using Lab3.WebApi.Models.Resources;

namespace Lab3.WebApi.Formatters
{
    public class StudentResourceFormatter : BufferedMediaTypeFormatter
    {
        public StudentResourceFormatter() 
            : base()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+xml"));
        }

        public override bool CanReadType(Type type) => false;

        public override bool CanWriteType(Type type) =>
            type.BaseType == typeof(StudentResource) || type.BaseType.GetGenericTypeDefinition() == typeof(LinkedResourceCollection<StudentResource>);

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            var encoding = base.SelectCharacterEncoding(content.Headers);

            var settings = new XmlWriterSettings();
            settings.Encoding = encoding;

            var writer = XmlWriter.Create(writeStream, settings);

            var resource = (LinkedResource)value;
            if (resource is LinkedResourceCollection<StudentResource>)
            {
                writer.WriteStartElement("resource");
                // writer.WriteAttributeString("href", resource.Href);

                foreach (LinkedResource innerResource in (IEnumerable)resource)
                {
                    SerializeInnerResource(writer, innerResource);
                }

                writer.WriteEndElement();
            }
            else
            {
                SerializeInnerResource(writer, resource);
            }

            writer.Flush();
            writer.Close();
        }

        private void SerializeInnerResource(XmlWriter writer, LinkedResource innerResource)
        {
            var a = 0;
            throw new NotImplementedException();
        }
    }
}