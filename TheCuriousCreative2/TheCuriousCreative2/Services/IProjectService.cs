using System;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.Services
{
    public interface IProjectService
    {
        //Get Projects
        Task<List<ProjectModel>> GetProjectList();

        //Add Projects
        Task<int> AddProject(ProjectModel projectModel);

        //Delete Projects
        Task<int> DeleteProject(ProjectModel projectModel);

        //Edit Projects
        Task<int> EditProject(ProjectModel projectModel);
    }
}

