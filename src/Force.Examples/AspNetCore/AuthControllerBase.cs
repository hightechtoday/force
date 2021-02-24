using Force.Examples.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Force.Examples.AspNetCore
{
    [ApiController]
    [ApiControllerFilter]
    [Route("/api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthControllerBase : ControllerBase
    {
    }
}