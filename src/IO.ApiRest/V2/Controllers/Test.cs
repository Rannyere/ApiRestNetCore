using System;
using Elmah.Io.AspNetCore;
using IO.ApiRest.Controllers;
using IO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IO.ApiRest.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class TestConnectionController : MainController
    {
        public TestConnectionController(INotifier notifier) : base(notifier) { }

        [HttpGet]
        public string Value()
        {
            //Tests by Provider Elmah.io

            //throw new Exception("Error");

            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{
            //    e.Ship(HttpContext);
            //}


            return "Test Swagger V2";
        }
    }
}