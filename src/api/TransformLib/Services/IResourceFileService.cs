using System.Collections.Generic;
using TransformLib.Transforms;

namespace TransformLib.Services
{
    public interface IResourceFileService
    {
        IList<DataElement> GetOriginalElements();

        IList<DataElement> GetTransformedElements(ITransformSettings transformSettings);

        void Read(string filePath);

        void Save(string filePath);
    }
}