using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Business.Interfaces;
using IO.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace IO.ApiRest.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotifier();
        }

        protected ActionResult CustomResponse( object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificationErrorInvalidModel(modelState);
            return CustomResponse();
        }

        protected void NotificationErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifierError(errorMsg);
            }
        }

        protected void NotifierError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
