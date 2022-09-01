using System;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.Services
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetProjectList();
        Task<int> AddProject(ProjectModel projectModel);
        Task<int> DeleteProject(ProjectModel projectModel);
        Task<int> UpdateProject(ProjectModel projectModel);
    }
}

