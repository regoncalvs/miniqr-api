using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MiniQr.API
{
  /// <summary>
  /// Define a classe AuthResponsesFilter para filtros de autenticação
  /// </summary>
  public class AuthResponsesFilter : IOperationFilter
  {
    /// <summary>
    /// Aplica o filtro adicionando configurações de autenticação para endpoints Authorize.
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      var hasAuthorize =
        context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
        .Union(context.MethodInfo.GetCustomAttributes(true))
        .OfType<AuthorizeAttribute>()
        .Any();

      if (hasAuthorize ?? false)
      {
        operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
        operation.Security = new List<OpenApiSecurityRequirement>
        {
          new OpenApiSecurityRequirement
          {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                {
                  Id = "Bearer",
                  Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
              },
              new List<string>()
            }
          }
        };
      }
    }
  }
}
