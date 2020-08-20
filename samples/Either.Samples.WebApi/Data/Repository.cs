using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Either.Samples.WebApi.Data
{
    public interface IRepository
    {
        Either<Guid, ArgumentException, InvalidDataContractException> Create(string email, string name);
        IEnumerable<User> ListUsers();
        Either<User, NullReferenceException> User(Guid id);
    }

    public class Repository : IRepository
    {
        private List<User> Users;
        public Repository()
        {
            Users = new()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "mike@test.com",
                    Name = "Mike"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "tim@test.com",
                    Name = "Time"
                }
            };
        }

        public IEnumerable<User> ListUsers() => Users;

        public Either<User, NullReferenceException> User(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                return new NullReferenceException();

            return user;

        }

        public Either<Guid, ArgumentException, InvalidDataContractException> Create(string email, string name)
        {
            //HACK: Need to actually write Regex Utils
            if (!email.Contains("@"))
                return new InvalidDataContractException("User email must be a valid email address.");

            if (string.IsNullOrWhiteSpace(name))
                return new ArgumentException("User property name must not be null or empty.");

            var user = new User { Id = Guid.NewGuid(), Email = email, Name = name, };
            Users.Add(user);
            return user.Id;
        }
    }
}
