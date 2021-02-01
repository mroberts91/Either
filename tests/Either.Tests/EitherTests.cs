using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using Xunit;

namespace Either.Tests
{
    public class EitherTests
    {
        [Fact]
        public void OnSuccess_Valid() =>
            Process(true).Resolve(
                user => StatusCodes.Status200OK,
                error => StatusCodes.Status400BadRequest
            )
            .ShouldBe(StatusCodes.Status200OK);

        [Fact]
        public void OnSuccess_Valid_Pattern() =>
            (Process(true).Value switch
            {
                User => StatusCodes.Status200OK,
                InvalidEmailException => StatusCodes.Status400BadRequest
            })
            .ShouldBe(StatusCodes.Status200OK);

        [Fact]
        public void OnError_Valid() =>
            Process().Resolve(
                user => StatusCodes.Status200OK,
                error => StatusCodes.Status400BadRequest
            )
            .ShouldBe(StatusCodes.Status400BadRequest);

        [Fact]
        public void OnError_Valid_Pattern() =>
            (Process().Value switch
            { 
                User => StatusCodes.Status200OK,
                InvalidEmailException => StatusCodes.Status400BadRequest
            })
            .ShouldBe(StatusCodes.Status400BadRequest);

        [Fact]
        public void OnSuccess_Invalid() =>
            Should.Throw(() => Process(false).Resolve(null, error => StatusCodes.Status400BadRequest), typeof(NullReferenceException));

        [Fact]
        public void OnError_Invalid() =>
            Should.Throw(() => Process(true).Resolve<int>(null,null) ,typeof(NullReferenceException));

        private static Either<User, InvalidEmailException> Process(bool? isValidEmail = null) =>
            isValidEmail is null ? new InvalidEmailException() : TestUser;

        private static User TestUser => new User("Test User", "test@test.com");

        private record User(string FullName, string Email);
        
        private class InvalidEmailException : Exception { }
    }
}
