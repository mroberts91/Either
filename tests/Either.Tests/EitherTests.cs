using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using Xunit;

namespace Either.Tests
{
    public class EitherTests
    {
        [Fact]
        public void OnSuccess_Valid()
        {
            var result = Process();

            var httpStatusCode = result.Resolve((
                user => StatusCodes.Status200OK,
                error => StatusCodes.Status400BadRequest
            ));

            httpStatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Fact]
        public void OnError_Valid()
        {
            var result = Process(true);

            var httpStatusCode = result.Resolve((
                user => StatusCodes.Status200OK,
                error => StatusCodes.Status400BadRequest
            ));

            httpStatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void OnSuccess_Invalid()
        {
            var result = Process();

            Should.Throw(() =>
            {
                result.Resolve((
                null,
                error => StatusCodes.Status400BadRequest
            ));
            }, typeof(InvalidOperationException));

        }

        [Fact]
        public void OnError_Invalid()
        {
            var result = Process();

            Should.Throw(() =>
            {
                result.Resolve<int>((
                null,
                null
            ));
            }, typeof(InvalidOperationException));
        }

        private Either<User, InvalidEmailException> Process(bool? isValidEmail = null)
        {
            if(isValidEmail == true)
                return new InvalidEmailException();

            return TestUser;
        }

        private User TestUser => new User { FullName = "Test User", Email = "test@test.com" };

        private class User
        {
            public string FullName { get; set; }

            public string Email { get; set; }
        }

        private class InvalidEmailException : Exception { }
    }
}
