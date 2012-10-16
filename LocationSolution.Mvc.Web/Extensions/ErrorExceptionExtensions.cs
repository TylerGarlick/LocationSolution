using NexBusiness.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class ErrorExceptionExtensions
{
    public static void ToModelState(this ErrorException errorException, Controller controller)
    {
        if(errorException != null)
        {
            foreach (var error in errorException.Errors)
                controller.ModelState.AddModelError(error.Property, error.Message);
        }
    }
}