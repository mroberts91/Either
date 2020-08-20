# Either
![Nuget](https://img.shields.io/nuget/v/Option.Either?style=flat-square)
![GitHub](https://img.shields.io/github/license/mroberts91/Either)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/mroberts91/Either/master)

Creates a simple struct that can be used as a discriminated union like return type, supporting the successful result as well as up to four additonal exceptions that can be returned.

### Example using the built in ```Resolve``` method
```C#
public IActionResult Get(Guid id)
{
   return  _repository.User(id)
                      .Resolve<IActionResult>((
                          user => Ok(user),
                          error => NotFound()
                      ));
}
```
### Example using C# pattern mathcing
```C#
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
```
