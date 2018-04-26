using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TransformLib.Services
{
    public class ProjectInfoService : IProjectInfoService
    {
        /// <inheritdoc/>
        public Task<IList<string>> GetProjectsAsync(string solutionPath)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IDictionary<string, IList<string>>> GetResourceFilesAsync(string solutionPath)
        {
            throw new NotImplementedException();
        }
    }
}