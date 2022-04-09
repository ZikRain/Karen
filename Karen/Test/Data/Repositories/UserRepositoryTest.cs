using Karen.Models.Entities;
using Karen.Models.Repositories;
using System;
using Xunit;

namespace Test.Data.Repositories
{
    public class UserRepositoryTest: BaseRepositoryTest
    {
        [Fact]
        public void GetByIdTest()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                var user = new User()
                {
                    user_id = 1535,
                    user_login = "Karen",
                    user_password = "123123123"
                };

               var newUser = userRepository.AddNewUser(user.user_login, user.user_password);


                Assert.NotNull(newUser);
            }
        }
    }
}
