﻿using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Force.Demo.Web
{
    // https://stackoverflow.com/questions/3290182/rest-http-status-codes-for-failed-validation-or-invalid-duplicate
    internal class ValidationFailedResult: JsonResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(modelState.Select(x => new
                {
                    x.Key,
                    ValidationState = x.Value.ValidationState.ToString(),
                    x.Value.Errors
                }).ToList())        
        {
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
            SetStatusCodeAndHeaders(context.HttpContext);
        }

        internal static void SetStatusCodeAndHeaders(HttpContext context)
        {
            context.Response.StatusCode = 422;
            context.Response.Headers.Add("X-Status-Reason", "Validation failed");
        }
    }
}