# gravel.BackEnd
.Net , C# , Dapper , Unit of Work , Unity , OAuth 2.0 , Identity

ref: 

1. https://github.com/EloreTec/UnitOfWorkWithDapper

2. https://github.com/twMVC/twMVC-18-1


## Set a connection string in Web.Config:

```sh
  <add name="Default" connectionString="xxxxx" providerName="System.Data.SqlClient" />
```


## OAuth Token with Role:

ApplicationOAuthProvider
```sh
  var identity = new ClaimsIdentity(context.Options.AuthenticationType);
  identity.AddClaim(new Claim(ClaimTypes.Role, "USER"));
  context.Validated(identity);
```
Controller
```sh
  [Authorize(Roles ="USER")]
  public class ValuesController : ApiController
  {
    ...
  }
```
