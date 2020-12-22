using System;
using IO.ApiRest.Controllers;
using IO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IO.ApiRest.V1.Controllers
{
    [Obsolete]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class TestConnectionController : MainController
    {
        public TestConnectionController(INotifier notifier) : base(notifier) { }

        [HttpGet]
        public string Value()
        {
            return "Test Swagger V1";
        }
    }
}