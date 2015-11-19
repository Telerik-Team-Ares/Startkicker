using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Startkicker.Services.Data.Tests.TestObjects;
using Startkicker.Data.Models;
using Startkicker.Services.Data.Contracts;

namespace Startkicker.Services.Data.Tests
{
    [TestClass]
    public class ProjectServiceTests
    {
        private IProjectsService projectService;
        //Ctrl-R Ctrl-T
        private InMemoryRepository<Project> projectsRepository;
        private InMemoryRepository<User> usersRepository;

        [TestInitialize]
        public void Init()
        {
            this.projectsRepository = TestObjectFactory.GetProjectsRepository();
            this.usersRepository = TestObjectFactory.GetUsersRepository();

            this.projectService = new ProjectsService(this.projectsRepository, this.usersRepository);
        }
        
        //Add
        [TestMethod]
        public void AddShouldAddProjectsCorrectly()
        {
            var projectToAdd = new Project()
            {
                Id = 5,
                Name = "John",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312
            };
            var result = projectService.Add(projectToAdd);
            Assert.AreEqual(result, projectToAdd.Id);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var projectToAdd = new Project()
            {
                Id = 5,
                Name = "John",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312
            };
            var result = projectService.Add(projectToAdd);

            Assert.AreEqual(1, this.projectsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateUserAndProjectToDatabase()
        {
            var projectToAdd = new Project()
            {
                Id = 5,
                Name = "John",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312
            };

            var result = this.projectService.Add(projectToAdd);

            var project = this.projectsRepository.All().FirstOrDefault(pr => pr.Name == "John");

            Assert.IsNotNull(project);
            Assert.AreEqual("John", project.Name);
            Assert.AreEqual("This is my description", project.Description);
            Assert.AreEqual(321312, project.GoalMoney);
        }

        // Update
        [TestMethod]
        public void UpdateShouldUpdateProjectsCorrectly()
        {
            var projectToAdd = new Project()
            {
                Name = "John",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var result = projectService.Add(projectToAdd);

            projectToAdd.Name = "Pesho";
            projectService.Update(projectToAdd);

            var oldName = "John";
            var expectedName = this.projectsRepository.UpdatedEntities[0].Name;

            Assert.AreNotEqual(expectedName, oldName);
            Assert.AreEqual(expectedName, "Pesho");
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var projectToAdd = new Project()
            {
                Name = "John",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var result = projectService.Add(projectToAdd);

            projectToAdd.Name = "Pesho";
            projectService.Update(projectToAdd);

            // One for Add and one for Update
            Assert.AreEqual(2, projectsRepository.NumberOfSaves);
            
        }

        // Get All
        [TestMethod]
        public void GetAllShouldReturnAllTheInsertedProjects()
        {
            var getAll = projectService.GetAll(1, int.MaxValue);

            // Initial add in TestObjectFactory is 25
            var expected = 25;

            Assert.AreEqual(expected, getAll.Count());
        }

        // Get by ID
        [TestMethod]
        public void GetByIdShouldReturnTheCorrectProject()
        {
            //The first added project
            var getted = projectService.GetById(0);


            // Initial add in TestObjectFactory is 25, we're taking the first
            var expectedName = TestConstants.InitialNameForProjectsCreation + 0;
            var expectedGoalMoney = TestConstants.InitialGoalMoneyForProjectsCreation;
            var expectedDescription = TestConstants.InitialDescriptionForProjectCreation + 0;

            Assert.AreEqual(expectedName, getted.Name);
            Assert.AreEqual(expectedGoalMoney, getted.GoalMoney);
            Assert.AreEqual(expectedDescription, getted.Description);
        }

        // Remove
        [TestMethod]
        public void RemoveShouldRemoveAProjectCorrectly()
        {
            var projectToRemove = new Project()
            {
                Name = "John1337",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var added = projectService.Add(projectToRemove);
            projectService.Remove(projectToRemove);

            // When we update a project we move it to the updated list
            // Since removing isn't deleting, but setting a isRemoved to true
            // We have to do this
            var updated = projectsRepository.UpdatedEntities.Select(x => x.IsRemoved == true).Count();

            Assert.AreEqual(1, updated);
            Assert.IsFalse(projectService.GetAll().Any(x => x.Name == "John1337"));
        }

        [TestMethod]
        public void RemoveShouldInvokeSaveChanges()
        {
            var projectToRemove = new Project()
            {
                Name = "John1337",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var initNumberOFSaves = 0;
            var added = projectService.Add(projectToRemove);
            initNumberOFSaves = projectsRepository.NumberOfSaves;

            projectService.Remove(projectToRemove);
            Assert.AreEqual(initNumberOFSaves+1, projectsRepository.NumberOfSaves);
        }

        // Remove by ID
        [TestMethod]
        public void RemoveByIDShouldWorkCorrectly()
        {
            var projectToRemove = new Project()
            {
                Name = "John1337",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var added = projectService.Add(projectToRemove);
            projectService.RemoveById(projectToRemove.Id);

            var updated = projectsRepository.UpdatedEntities.Select(x => x.IsRemoved == true).Count();

            Assert.AreEqual(1, updated);
            Assert.IsFalse(projectService.GetAll().Any(x => x.Name == "John1337"));
        }

        [TestMethod]
        public void RemoveByIDShouldInvokeSaveChanges()
        {

            var projectToRemove = new Project()
            {
                Name = "John1337",
                EstimatedDate = DateTime.Now,
                Description = "This is my description",
                GoalMoney = 321312,
                Id = 1337
            };
            var initNumberOFSaves = 0;
            var added = projectService.Add(projectToRemove);
            initNumberOFSaves = projectsRepository.NumberOfSaves;

            projectService.RemoveById(projectToRemove.Id);
            Assert.AreEqual(initNumberOFSaves + 1, projectsRepository.NumberOfSaves);
        }

        // Add Money

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void AddMoneyShouldThrowIfNoUserIsPassed()
        {
            var result = projectService.AddMoney(0, 100,null);
        }

        [TestMethod]
        public void AddMoneyShouldReturnZeroIfUserDoesNotHaveEnoughMoney()
        {
            var userID = TestConstants.InitialUserID + 0;
            var result = projectService.AddMoney(0, int.MaxValue, userID);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AddMoneyShouldTakeAwayMoneyCorrectly()
        {
            var userID = TestConstants.InitialUserID + 0;
            var amount = 250;
            var result = projectService.AddMoney(0, amount, userID);

            var user = usersRepository.GetById(TestConstants.InitialUserID + 0);

            Assert.AreEqual(TestConstants.InitialUserCreationMoneyAmount - amount, user.MoneyAmount);
        }

        [TestMethod]
        public void AddMoneyShouldInvokeSaveChangesTwice()
        {
            var userID = TestConstants.InitialUserID + 0;
            var amount = 250;
            var result = projectService.AddMoney(0, amount, userID);

            var invokedForProject = projectsRepository.NumberOfSaves;
            var invokedForUser = usersRepository.NumberOfSaves;

            Assert.AreEqual(2, invokedForProject + invokedForUser);
        }
    }
}
