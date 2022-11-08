using Docati.Api;
using System;
using System.IO;
using System.Text;

namespace Demo.Docati.DocumentGeneration.Lib
{
    public class DocumentGenerator
    {
        public static byte[] GenerateDocument(string templatePath, string templateFileName, string templateData, GeneratedDocumentType generatedDocType)
        {
            ResourceProvider rp = new ResourceProvider(new Uri(templatePath), false);
            using (var templateDataStream = new MemoryStream(Encoding.UTF8.GetBytes(templateData)))
            {
                using (var outputDocumentStream = new MemoryStream())
                {
                    using (var builder = new DocBuilder(templateFileName, rp))
                    {
                        DocumentFileFormat docFormat = generatedDocType == GeneratedDocumentType.PDF ? DocumentFileFormat.PDF : DocumentFileFormat.Word;
                        builder.Build(templateDataStream, DataFormat.Xml, outputDocumentStream, null, docFormat);
                    }
                    return outputDocumentStream.ToArray();
                }
            }
            
        }
    }
}
