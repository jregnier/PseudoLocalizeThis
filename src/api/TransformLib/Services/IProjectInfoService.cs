using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransformLib.Services
{
    public interface IProjectInfoService
    {
        /// <summary>
        /// Gets a list of all projects in a solution.
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> GetProjectsAsync(string solutionPath);

        /// <summary>
        /// Gets a dictionary of all resource files in a solution grouped by project.
        /// </summary>
        /// <returns></returns>
        Task<IDictionary<string, IList<string>>> GetResourceFilesAsync(string solutionPath);
    }
}
