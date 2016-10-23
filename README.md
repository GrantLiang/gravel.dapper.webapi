# gravel.dapper.webapi
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

## Unity Dependency and Unit of Work:

RegisterType : UnityConfig.cs
```sh
    var connectionString = "Default";

    container
        .RegisterType<IDbContextFactory, DbContextFactory>(
            new PerRequestLifetimeManager(),
            new InjectionConstructor(connectionString))
        .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
        .RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new PerRequestLifetimeManager())
        //.RegisterType<IEmployeeManager, EmployeeManager>(new PerRequestLifetimeManager())
        .RegisterTypes(
            AllClasses.FromAssemblies(true, Assembly.Load("gravel.core")),
            WithMappings.FromMatchingInterface,
            WithName.Default,
            WithLifetime.Custom<PerRequestLifetimeManager>)
        .RegisterTypes(
            AllClasses.FromAssemblies(true, Assembly.Load("gravel.service")),
            WithMappings.FromMatchingInterface,
            WithName.Default,
            WithLifetime.Custom<PerRequestLifetimeManager>);
        }
    }
```

RegisterType : UnitOfWorkAttribute.cs
```sh
    // Start a context transaction
    public override void OnActionExecuting(HttpActionContext filterContext)
    {
        this._unitOfWork = UnitOfWorkFactory.Create();
        this._unitOfWork.Context.BeginTransaction();
        base.OnActionExecuting(filterContext);
    }

    // End.
    public override void OnActionExecuted(HttpActionExecutedContext filterContext)
    {
        if (filterContext.Exception == null)
        {
            this._unitOfWork.SaveChange();
        }
        else
        {
            this._unitOfWork.RollBack();
        }
        base.OnActionExecuted(filterContext);
    }
```

Useage with UnitOfWork Attribute
```sh
    [Authorize(Roles ="USER")]
    public class ValuesController : ApiController
    {
        [Dependency]
        public IEmployeeManager _employeeRepository { get; set; }

        // GET api/values

        [UnitOfWork]
        public IList<Employees> Get()
        {
            return _employeeRepository.AddEmp();
        }
    }
```
