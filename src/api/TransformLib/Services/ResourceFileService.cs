using System;
using System.Collections.Generic;
using TransformLib.Transforms;

namespace TransformLib.Services
{
    public class ResourceFileService : IResourceFileService, IDisposable
    {
        private ResxFileTransform _resxFileTransform;

        public IList<DataElement> GetOriginalElements()
        {
            if (_resxFileTransform == null)
            {
                throw new Exception("You must first read a file");
            }

            return _resxFileTransform.Items;
        }

        public IList<DataElement> GetTransformedElements(
            ITransformSettings transformSettings)
        {
            if (_resxFileTransform == null)
            {
                throw new Exception("You must first read a file");
            }

            _resxFileTransform.TransformValues(transformSettings);

            return _resxFileTransform.TransformedItems;
        }

        public void Read(string filePath)
        {
            _resxFileTransform?.Dispose();
            _resxFileTransform = ResxFileTransform.Read(filePath);
        }

        public void Save(string filePath)
        {
            if (_resxFileTransform == null)
            {
                throw new Exception("You must first read a file");
            }

            if (!_resxFileTransform.IsTransformed)
            {
                throw new Exception("You must first apply a transform to the selected resource file.");
            }

            _resxFileTransform.WritePseudoLocalizedFile(filePath);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _resxFileTransform?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}