using Either.Samples.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace Either.Samples.WebApi.Controllers
{
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository _respository;
        public UsersController(IRepository repository)
        {
            _respository = repository;
        }
        
        [HttpGet]
        public IActionResult Get() 
            => Ok(_respository.ListUsers());
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
            => _respository.User(id).Resolve<IActionResult>((
                    user => Ok(user),
                    error => NotFound()
                ));

        [HttpPost]
        public IActionResult Post([FromBody] UserCreateRequest request)
            => _respository.Create(request.Email, request.Name)
                           .Resolve<IActionResult>((
                                guid => Created(guid.ToString(), guid),
                                nameExp => BadRequest(new { nameExp.Message, request }),
                                emailExp => BadRequest(new { emailExp.Message, request })
                            ));
    }

    public class UserCreateRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
