using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TransformLib.Transforms
{
    public class ResxFileTransform : IDisposable
    {
        private Stream _stream;
        private MemoryStream _transformedStream;

        public IList<DataElement> Items { get; private set; }

        public IList<DataElement> TransformedItems { get; private set; }

        public bool IsTransformed { get; private set; }

        public static ResxFileTransform Read(string filePath)
        {
            var resx = new ResxFileTransform();
            resx.ReadResxFile(filePath);

            return resx;
        }

        public void ReadResxFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            _stream = new FileStream(filePath, FileMode.Open);

            Items = (from d in XDocument.Load(_stream).Root.Descendants("data")
                     select new DataElement()
                     {
                         Name = d.Attribute("name")?.Value,
                         Value = d.Element("value")?.Value,
                         Comment = d.Element("comment")?.Value
                     })
                    .ToList();
        }

        public void TransformValues(
            ITransformSettings settings)
        {
            if (_stream == null)
            {
                throw new Exception("A Resx file must be read first");
            }
            
            _stream.Seek(0, SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            _stream.CopyTo(tempStream);
            tempStream.Seek(0, SeekOrigin.Begin);
            TransformedItems = new List<DataElement>();

            var doc = XDocument.Load(tempStream);

            foreach (var element in doc.Root.Descendants("data"))
            {
                IPseudoTransform pseudoString = new PseudoString(element.Element("value").Value);

                if (settings.Brackets)
                {
                    pseudoString = new BracketTransform(pseudoString);
                }

                if (settings.Larger)
                {
                    pseudoString = new LargerTransform(pseudoString);
                }

                if (settings.Mirror)
                {
                    pseudoString = new MirrorTransform(pseudoString);
                }

                element.Element("value").Value = pseudoString.Transform();

                TransformedItems.Add(new DataElement()
                {
                    Name = element.Attribute("name")?.Value,
                    Value = element.Element("value").Value,
                    Comment = element.Element("comment")?.Value
                });
            }

            _transformedStream = new MemoryStream();
            doc.Save(_transformedStream);

            IsTransformed = true;
        }

        public void WritePseudoLocalizedFile(string filePah)
        {
            if (!IsTransformed)
            {
                return;
            }


            using (var outStream = new FileStream(filePah, FileMode.Create, FileAccess.Write))
            {
                _transformedStream.Seek(0, SeekOrigin.Begin);
                _transformedStream.WriteTo(outStream);
            }
        }

        public string GetPseudoLocalizedString()
        {
            if (!IsTransformed)
            {
                return string.Empty;
            }

            _transformedStream.Seek(0, SeekOrigin.Begin);
            return _transformedStream.ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _stream?.Dispose();
                    _transformedStream?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}