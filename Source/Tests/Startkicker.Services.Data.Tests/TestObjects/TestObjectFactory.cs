namespace Startkicker.Services.Data.Tests.TestObjects
{
    using System;
    using Startkicker.Data.Models;

    public static class TestObjectFactory
    {
        public static InMemoryRepository<Project> GetProjectsRepository(int numberOfProjects = TestConstants.InitialNumberForCreations)
        {
            var repo = new InMemoryRepository<Project>();

            for (int i = 0; i < numberOfProjects; i++)
            {
                var date = new DateTime(2015, 11, 5, 23, 47, 12);
                date.AddDays(i);

                repo.Add(new Project
                {
                    Id = i,
                    EstimatedDate = date,
                    Description = TestConstants.InitialDescriptionForProjectCreation + i,
                    Name = TestConstants.InitialNameForProjectsCreation + i,
                    GoalMoney = TestConstants.InitialGoalMoneyForProjectsCreation                    
                });
            }

            return repo;
        }

        public static InMemoryRepository<User> GetUsersRepository(int numberOfUsers = TestConstants.InitialNumberForCreations)
        {
            var repo = new InMemoryRepository<User>();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var date = new DateTime(2015, 11, 5, 23, 47, 12);
                date.AddDays(i);

                repo.Add(new User
                {
                    Id = TestConstants.InitialUserID + i,
                    FirstName = TestConstants.InitialUserCreationFirstName + i,
                    LastName = TestConstants.InitialUserCreationLastName + i,
                    MoneyAmount = TestConstants.InitialUserCreationMoneyAmount,
                    UserName = TestConstants.InitialUserCreationUserName + i,
                    Email = TestConstants.InitialUserCreationEmail + i,
                    PasswordHash = Guid.NewGuid().ToString()                    
                });
            }

            return repo;
        }
    }
}
