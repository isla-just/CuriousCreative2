using System;
using SQLite;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.Services
{
    public class ProjectService : IProjectService
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Project.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<ProjectModel>();
            }
        }

        public async Task<int> AddProject(ProjectModel projectModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(projectModel);
        }

        public async Task<int> DeleteProject(ProjectModel projectModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(projectModel);
        }

        public async Task<List<ProjectModel>> GetProjectList()
        {
            await SetUpDb();
            var projectList = await _dbConnection.Table<ProjectModel>().ToListAsync();
            return projectList;
        }

        public async Task<int> EditProject(ProjectModel projectModel)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(projectModel);
        }
    }
}