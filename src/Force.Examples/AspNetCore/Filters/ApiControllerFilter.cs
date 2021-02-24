using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Force.Examples.AspNetCore.Filters
{
    public class ApiControllerFilter : ValidationAttribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dtos = context
                .ActionDescriptor
                .Parameters
                .Where(x => x.BindingInfo?.BindingSource != BindingSource.Services)
                .ToList();

            if (!dtos.Any())
            {
                await next();
                return;
            }

            if (!context.ModelState.IsValid)
            {
                var attributesErrors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => new ValidationResult(
                        x.Value.Errors.Count == 1
                            ? x.Value.Errors.Single().ErrorMessage
                            : x.Value.Errors.Aggregate("", (a, c) => $"{c.ErrorMessage}.{a}"),
                        new List<string> {x.Key}))
                    .ToList();
                
                context.Result = new JsonResult(attributesErrors)
                {
                    StatusCode = 422,
                };
                return;
            }

            var arguments = context
                .ActionArguments
                .Join(dtos, x => x.Key, y => y.Name, (x, y) => new {entity = x.Value, type = y.ParameterType})
                .ToList();

            if (!arguments.Any())
            {
                await next();
                return;
            }

            var errors = arguments
                .Select(x =>
                    new
                    {
                        validator =
                            context.HttpContext.RequestServices.GetService(
                                typeof(IValidator<>).MakeGenericType(x.type)),
                        x.entity
                    }
                )
                .Where(x => x.validator != null)
                .Select(x => ((dynamic) x.validator).Validate((dynamic) x.entity))
                .Where(x => x != null)
                .Cast<IEnumerable<ValidationResult>>()
                .SelectMany(x => x).ToList();
            

            if (errors.Any())
            {
                context.Result = new JsonResult(errors)
                {
                    StatusCode = 422,
                };
                return;
            }

            await next();
        }
    }
}