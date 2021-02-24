﻿using Microsoft.AspNetCore.Mvc;

 namespace Force.Examples.AspNetCore
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
    }
}