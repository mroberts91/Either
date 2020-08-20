using Either.Samples.WebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Either.Samples.WebApi.Controllers
{
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository _repository;
        public UsersController(IRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult Get() 
            => Ok(_repository.ListUsers());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
           return  _repository.User(id)
                              .Resolve<IActionResult>((
                                user => Ok(user),
                                error => NotFound()
                            ));
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserCreateRequest request)
        {
            var result = _repository.Create(request.Email, request.Name);
                
            return result.Value switch
            {
                Guid guid =>  Created(guid.ToString(), guid),
                ArgumentException ex => BadRequest(new { ex.Message, request }),
                InvalidDataContractException ex => BadRequest(new { ex.Message, request }),
                _ => ServerError()
            };
        }

        private IActionResult ServerError()
            => StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Unable to process request at this time." });

    }

    public class UserCreateRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
